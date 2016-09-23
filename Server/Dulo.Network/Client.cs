using System;
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

        private IPEndPoint serverIp;

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

            headChecker.Add(BaseHeaders.Ping, HeadCheckerMessagePing);

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

            headChecker.Check(model, null);

            ReciveData?.Invoke(model, ipEndPoint);
        }

        public void Connect(IPEndPoint serverIp)
        {
            this.serverIp = serverIp;

            resenderConnect.Start(SendConnect);
        }

        public void Connect(string ipAddress, int port)
        {
            Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
        }

        private void SendConnect()
        {
            SendData<string>(BaseHeaders.Connect, "", serverIp);
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
            SendData<string>(BaseHeaders.Ping, "", serverIp);
        }

        private void ConnectionLost()
        {
            OnConnectionLost?.Invoke();
        }

        #region HeadCheckerMethods

        private void HeadCheckerMessagePing(MessageModel model, object arg)
        {
            Ping = DateTime.Now.ToMilliseconds() - ms;
            resenderPing.Stop();
            OnPingChanged?.Invoke();
        }

        private void HeadCheckerMessageConnectionSuccess(MessageModel model, object arg)
        {
            resenderConnect.Stop();
            OnConnectionSuccess?.Invoke();
        }

        private void HeadCheckerMessageConnectionDenied(MessageModel model, object arg)
        {
            resenderConnect.Stop();
            serverIp = null;
            OnConnectionDenied?.Invoke();
        }        

        #endregion
    }
}
