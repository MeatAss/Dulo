using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Dulo.Network.Models;

namespace Dulo.Network
{
    public class Server : BaseClient
    {
        public event ReciveMessage ReciveMessage;

        protected List<IPEndPoint> clients = new List<IPEndPoint>();

        public int MaxConnection { get; set; } = 10;

        public Server() : base ()
        {
            resiveData += ServerReciveData;
        }

        public Server(int listenerPort) : base(listenerPort)
        {
            resiveData += ServerReciveData;
        }

        private void ServerReciveData(string message, IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() => DataRoute(message, ipEndPoint));            
        }

        private void DataRoute(string message, IPEndPoint ipEndPoint)
        {
            var model = JsonConvert.DeserializeObject<MessageModel>(message);

            if (model.Head == BaseHeaders.Connect)
            {
                СonnectionProcessing(ipEndPoint);
                return;
            }
            
            if (model.Head == BaseHeaders.Ping)
            {
                SendData<string>(BaseHeaders.Ping, "", ipEndPoint);//
            }
            
            ReciveMessage?.Invoke(message, ipEndPoint);
        }

        private void СonnectionProcessing(IPEndPoint ipEndPoint)
        {
            if (CanConnectClient())
            {
                ConnectionAccept(ipEndPoint);
            }
            else
            {
                ConnectionDenied(ipEndPoint);
            }
        }

        private bool CanConnectClient()
        {
            return clients.Count() < MaxConnection;
        }

        private void ConnectionAccept(IPEndPoint ipEndPoint)
        {
            clients.Add(ipEndPoint);
            SendData<string>(BaseHeaders.ConnectionSuccess, "", ipEndPoint);
        }

        private void ConnectionDenied(IPEndPoint ipEndPoint)
        {
            SendData<string>(BaseHeaders.ConnectionDenied, "", ipEndPoint);
        }


    }
}
