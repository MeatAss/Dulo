﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Dulo.Network.Models;
using System.Threading;

namespace Dulo.Network
{
    public class Client : BaseClient
    {
        public event Action OnConnectionSuccess;
        public event Action OnConnectionDenied;
        public event Action OnConnectionLost;
        public event Action OnPingChanged;

        public long Ping { get; protected set; }

        protected IPEndPoint serverIP;
        public IPEndPoint ServerIP { get { return serverIP.Clone(); } }

        private long ms;
        
        private DataResender resenderPing;
        private DataResender resenderConnect;

        private HeadChecker headChecker;

        public event ReciveData ReciveData;

        public Client() : base ()
        {
            Initialize();
        }        

        public Client(int listenerPort) : base(listenerPort)
        {
            Initialize();
        }

        private void Initialize()
        {
            resiveData += ClientResiveData;
            StartListening();

            resenderPing = new DataResender(ConnectionLost);

            resenderConnect = new DataResender(() => OnConnectionDenied(), 5, 1000);

            StartListening();

            InitializeHeadChecker();
        }

        private void InitializeHeadChecker()
        {
            headChecker = new HeadChecker();            

            headChecker.Add(BaseHeaders.ConnectionSuccess, HeadCheckerMessageConnectionSuccess);

            headChecker.Add(BaseHeaders.ConnectionDenied, HeadCheckerMessageConnectionDenied);
        }

        private void ClientResiveData(string message, IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() => DataRoute(message, ipEndPoint));
        }

        private void DataRoute(string message, IPEndPoint ipEndPoint)
        {
            var model = JsonTransformer.DeserializeObject<MessageModel>(message);

            if (model == null)
            {
                HeadCheckerMessagePing(ipEndPoint);
                return;
            }

            if (headChecker.Check(model, null))
                return;

            ReciveData?.Invoke(model, ipEndPoint);
        }

        public void Connect(IPEndPoint serverIp)
        {
            this.serverIP = serverIp;

            resenderConnect.Start(SendConnect);
        }

        public void Connect(string ipAddress, int port)
        {
            Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
        }

        private void SendConnect()
        {
            SendData<string>(BaseHeaders.Connect, "", serverIP);
        }

        public void UpdatePing()
        {

            if (!resenderPing.Start(SendPing))
            {
                return;
            }
            
            ms = DateTime.Now.ToMilliseconds();
        }

        private void SendPing()
        {
            Send("", serverIP);
        }

        private void ConnectionLost()
        {
            OnConnectionLost?.Invoke();
        }

        #region HeadCheckerMethods

        private void HeadCheckerMessagePing(object arg)
        {
            Ping = DateTime.Now.ToMilliseconds() - ms;
            resenderPing.Stop();
            OnPingChanged?.Invoke();
        }

        private void HeadCheckerMessageConnectionSuccess(MessageModel model, object arg)
        {
            resenderConnect.Stop();
            UpdatePing();
            OnConnectionSuccess?.Invoke();
        }

        private void HeadCheckerMessageConnectionDenied(MessageModel model, object arg)
        {
            resenderConnect.Stop();
            serverIP = null;
            OnConnectionDenied?.Invoke();
        }        

        #endregion

        public void SendDataToServer<T>(byte head, T body) where T : class
        {
            SendData<T>(head, body, serverIP);
        }
    }
}
