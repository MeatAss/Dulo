using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Dulo.Network.Models;
using Newtonsoft.Json;

namespace Dulo.Network
{
    public delegate void ReciveMessage(string message, IPEndPoint ipEndPoint);

    public abstract class BaseClient
    {
        protected UdpClient listener;

        protected event ReciveMessage resiveData;

        public BaseClient()
        {
            listener = new UdpClient(5001);
        }

        public BaseClient(int listenerPort)
        {
            listener = new UdpClient(listenerPort);
        }

        public void SendData<T>(byte head, T body, IPEndPoint ipEndPoint) where T : class
        {
            var response = new MessageModel
            {
                Head = head,
                Body = JsonConvert.SerializeObject(body)
            };

            Send(JsonConvert.SerializeObject(response), ipEndPoint);
        }

        public void Send(string message, IPEndPoint ipEndPoint)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            listener.Send(bytes, bytes.Length, ipEndPoint);
        }

        public void Send(string message, long hostName, int port)
        {
            Send(message, new IPEndPoint(hostName, port)); 
        }        

        #region Listener

        public void StartListening()
        {
            Task.Factory.StartNew(ListenerBody);
        }

        private void ListenerBody()
        {
            while (true)
            {
                Listen();
            }
        }

        private void Listen()
        {
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0); 
            byte[] bytes = listener.Receive(ref remoteIPEndPoint);

            int test = remoteIPEndPoint.Port;
            string results = Encoding.UTF8.GetString(bytes);

            if (resiveData != null)
                resiveData(results, remoteIPEndPoint);
        }
        #endregion
    }
}
