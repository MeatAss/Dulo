using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Dulo.Network.Models;
using Newtonsoft.Json;
using System.Threading;

namespace Dulo.Network
{
    public class Client : BaseClient
    {
        public event Action OnConnectionSuccess;
        public event Action OnConnectionDenied;
        public event Action OnConnectionLost;
        public event Action OnPingChanged;

        public int Ping { get; protected set; }

        private IPEndPoint serverIp;

        private int ms;

        private bool isConnectionFound = true;

        private Recall recallPing;

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

            recallPing = new Recall();
            recallPing.OnDied += ConnectionLost;
        }

        private void ClientResiveData(string message, IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() => DataRoute(message, ipEndPoint));
        }

        private void DataRoute(string message, IPEndPoint ipEndPoint)
        {
            var model = JsonConvert.DeserializeObject<MessageModel>(message);

            if (model.Head == BaseHeaders.Ping)
            {
                Ping = DateTime.Now.Millisecond - ms;
                isConnectionFound = true;
                recallPing.Stop();
                OnPingChanged?.Invoke();
            }

            if (model.Head == BaseHeaders.ConnectionSuccess)
            {
                OnConnectionSuccess?.Invoke();
            }

            if (model.Head == BaseHeaders.ConnectionDenied)
            {
                OnConnectionDenied?.Invoke();
                serverIp = null;
            }            
        }

        public void Connect(IPEndPoint serverIp)
        {
            this.serverIp = serverIp;
            SendData<string>(BaseHeaders.Connect, "", serverIp);
        }

        public void UpdatePing()
        {

            if (!isConnectionFound)
            {
                return;
            }

            isConnectionFound = false;

            recallPing = new Recall();
            recallPing.OnDied += ConnectionLost;

            recallPing.Start(SendPing);
            ms = DateTime.Now.Millisecond;
        }

        private void SendPing()
        {
            SendData<string>(BaseHeaders.Ping, "", serverIp);
        }

        private void ConnectionLost()
        {
            CloseConnection();
            OnConnectionLost?.Invoke();
        }
    }
}
