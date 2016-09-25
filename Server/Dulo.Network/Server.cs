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
    public delegate void ClientStateChange(IPEndPoint ipEndPoint);

    public class Server : BaseClient
    {
        public event ReciveData ReciveData;

        protected List<ClientModel> clients = new List<ClientModel>();

        public event ClientStateChange OnClientConnect;
        public event ClientStateChange OnClientDiconnect;

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

            StartListening();
        }

        private void InitializeHeadChecker()
        {
            headChecker = new HeadChecker();

            headChecker.Add(BaseHeaders.Connect, HeadCheckerMessageConnect);
        }

        private void ServerReciveData(string message, IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() => DataRoute(message, ipEndPoint));            
        }

        private void DataRoute(string message, IPEndPoint ipEndPoint)
        {
            MessageModel model = JsonTransformer.DeserializeObject<MessageModel>(message);

            UpdateClientLastTime(ipEndPoint);

            if (model == null)
            {
                HeadCheckerMessagePing(model, ipEndPoint);
                UpdateClientLastTime(ipEndPoint);
                return;
            }

            if (headChecker.Check(model, ipEndPoint))
                return;

            ReciveData?.Invoke(model, ipEndPoint);
        }

        private void UpdateClientLastTime(IPEndPoint ipEndPoint)
        {
            Task.Factory.StartNew(() =>
            {
                var client = clients.FirstOrDefault((item) => item.ClientIp.Address.ToString() == ipEndPoint.Address.ToString());
                client?.UpdateTime();
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

        public void SendDataToClients<T>(byte head, T body) where T : class
        {
            clients.ForEach((item) => SendData<T>(head, body, item.ClientIp));
        }

        private void ConnectionAccept(IPEndPoint ipEndPoint)
        {
            clients.Add(new ClientModel(ipEndPoint));

            OnClientConnect?.Invoke(ipEndPoint);
            
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
                    .ForEach((itemDelete) =>
                    {
                        clients.Remove(itemDelete);
                        OnClientDiconnect?.Invoke(itemDelete.ClientIp);
                    });
            }
        }

        public IPEndPoint GetClientIP(int index)
        {
            if (index < 0 || index >= clients.Count)
                return null;
                        
            return clients[index].ClientIp.Clone();
        }

        public IEnumerable<IPEndPoint> GetClientsIP()
        {
            return clients.Select((x) => x.ClientIp.Clone());
        }

        public int GetClientsCount()
        {
            return clients.Count;
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
            Send("", (IPEndPoint)ipEndPoint);
        }

        #endregion
    }
}
