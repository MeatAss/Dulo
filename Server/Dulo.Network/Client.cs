using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Dulo.Network.Models;
using Newtonsoft.Json;

namespace Dulo.Network
{
    public class Client : BaseClient
    {
        public event Action OnConnectionSuccess;
        public event Action OnConnectionDenied;

        private IPEndPoint serverIp;
        private double ms;

        public Client() : base ()
        {
            resiveData += ClientResiveData;
            StartListening();
        }        

        public Client(int listenerPort) : base(listenerPort)
        {
            resiveData += ClientResiveData;
            StartListening();
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
                //
                //
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

        public void Ping()
        {
            //
            SendData<string>(BaseHeaders.Ping, "", serverIp);
            ms = DateTime.Now.Millisecond;
        }
    }
}
