using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Dulo.Network;
using System.Net;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static Dulo.Network.Client client;

        static void Main(string[] args)
        {
            client = new Dulo.Network.Client(5001);

            Console.WriteLine("write ip: ");
            //string ip = "25.99.35.165";
            string ip = Console.ReadLine();

            client.Connect(new IPEndPoint(IPAddress.Parse(ip), 5002));

            client.OnConnectionSuccess += Client_OnConnectionSuccess;
            client.OnConnectionDenied += Client_OnConnectionDenied;
            client.OnPingChanged += Client_OnPingChanged;
            client.OnConnectionLost += Client_OnConnectionLost;   

            Console.ReadLine();
        }

        private static void Client_OnConnectionLost()
        {
            Console.WriteLine("lost connection");
        }

        private static void Client_OnPingChanged()
        {
            Console.WriteLine("ping : " + client.Ping);

            Thread.Sleep(1000);
            client.UpdatePing();
        }

        private static void Client_OnConnectionDenied()
        {
            Console.WriteLine("not connected!! Please close");
            Console.ReadLine();
        }

        private static void Client_OnConnectionSuccess()
        {
            Console.WriteLine("Connected!");

            client.UpdatePing();
        }
    }
}
