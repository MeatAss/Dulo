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
    public class Server : BaseClient
    {
        public event ReciveMessage ReciveMessage;

        public List<ClientModel> clients = new List<ClientModel>();

        public int MaxConnection { get; set; } = 10;

        public int DeathTime = 10000;

        private HeadChecker headChecker;

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
            InitializeHeadChecker();
        }

        private void InitializeHeadChecker()
        {
            headChecker = new HeadChecker();

            headChecker.Add(BaseHeaders.Ping, HeadCheckerMessagePing);

            headChecker.Add(BaseHeaders.Connect, HeadCheckerMessageConnect);
        }

        private void ServerReciveData(string message, IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() => DataRoute(message, ipEndPoint));            
        }

        private void DataRoute(string message, IPEndPoint ipEndPoint)
        {
            MessageModel model = JsonTransformer.DeserializeObject<MessageModel>(message);

            if (model == null)
                return;

            UpdateClientLastTime(ipEndPoint);

            if (headChecker.Check(model, ipEndPoint))
                return;

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

        #region HeadCheckerMethods

        private void HeadCheckerMessageConnect(MessageModel model, object ipEndPoint)
        {
            var client = clients.FirstOrDefault((item) => item.ClientIp.Equals((IPEndPoint)ipEndPoint));
            if (clients != null)
                clients.Remove(client);

            СonnectionProcessing((IPEndPoint)ipEndPoint);
        }

        private void HeadCheckerMessagePing(MessageModel model, object ipEndPoint)
        {
            SendData<string>(BaseHeaders.Ping, "", (IPEndPoint)ipEndPoint);
        }

        #endregion
    }
}
