using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Dulo.Network.Models;
using System.Threading;

namespace Dulo.Network
{
    public class Server : BaseClient
    {
        public event ReciveMessage ReciveMessage;

        public List<ClientModel> clients = new List<ClientModel>();

        public int MaxConnection { get; set; } = 10;

        public int DeathTime = 20000;

        public Server() : base ()
        {
            Initialize();
        }

        public Server(int listenerPort) : base(listenerPort)
        {
            Initialize();
        }

        private void Initialize()
        {
            resiveData += ServerReciveData;
            Task.Factory.StartNew(StartCheckClientsLastTime);
        }

        private void ServerReciveData(string message, IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() => DataRoute(message, ipEndPoint));            
        }

        private void DataRoute(string message, IPEndPoint ipEndPoint)
        {
            var model = JsonConvert.DeserializeObject<MessageModel>(message);

            if (model.Head == BaseHeaders.Ping)
            {
                SendData<string>(BaseHeaders.Ping, "", ipEndPoint);
            }

            if (model.Head == BaseHeaders.Connect)
            {
                СonnectionProcessing(ipEndPoint);
                return;
            }

            UpdateClientLastTime(ipEndPoint);

            ReciveMessage?.Invoke(message, ipEndPoint);
        }

        private void UpdateClientLastTime(IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() =>
            {
                var client = clients.FirstOrDefault((item) => item.ClientIp.Address.ToString() == ipEndPoint.Address.ToString());
                client.UpdateTime();
            });
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
            clients.Add(new ClientModel(ipEndPoint));
            
            SendData<string>(BaseHeaders.ConnectionSuccess, "", ipEndPoint);
        }

        private void ConnectionDenied(IPEndPoint ipEndPoint)
        {
            SendData<string>(BaseHeaders.ConnectionDenied, "", ipEndPoint);
        }

        private void StartCheckClientsLastTime()
        {
            while (true)
            {
                Thread.Sleep(1000);
                var timeNow = DateTime.Now.ToMilliseconds();
                clients.Where((item) => timeNow - item.ClientsLastMessageTime >= DeathTime)
                    .ToList()
                    .ForEach((itemDelete) => clients.Remove(itemDelete));                                
            }
        }
    }
}
