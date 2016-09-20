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

        public long Ping { get; protected set; }

        private IPEndPoint serverIp;

        private long ms;
        
        private DataResender resenderPing;
        private DataResender resenderConnect;

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
                Ping = DateTime.Now.ToMilliseconds() - ms;
                resenderPing.Stop();
                OnPingChanged?.Invoke();
            }

            if (model.Head == BaseHeaders.ConnectionSuccess)
            {
                resenderConnect.Stop();
                OnConnectionSuccess?.Invoke();
            }

            if (model.Head == BaseHeaders.ConnectionDenied)
            {
                resenderConnect.Stop();                
                serverIp = null;
                OnConnectionDenied?.Invoke();
            }            
        }

        public void Connect(IPEndPoint serverIp)
        {
            this.serverIp = serverIp;

            resenderConnect.Start(SendConnect);
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
            CloseConnection();
            OnConnectionLost?.Invoke();
        }
    }
}
