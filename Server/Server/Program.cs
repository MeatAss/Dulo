using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dulo.Network;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var cl1 = new Client(5001);
            var cl11 = new Client(5002);
            var cl111 = new Client(5003);
            var cl2 = new Dulo.Network.Server(5004);


            cl1.OnConnectionDenied += Cl1_OnConnectionDenied;
            cl11.OnConnectionDenied += Cl1_OnConnectionDenied;
            cl111.OnConnectionDenied += Cl1_OnConnectionDenied;
            cl1.OnConnectionSuccess += Cl1_OnConnectionSuccess;
            cl11.OnConnectionSuccess += Cl1_OnConnectionSuccess;
            cl111.OnConnectionSuccess += Cl1_OnConnectionSuccess;

            cl2.MaxConnection = 2;
            cl2.StartListening();

            cl1.Connect(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 5004));
            cl11.Connect(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 5004));
            cl111.Connect(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 5004));


            Console.ReadLine();

            //Console.WriteLine("write port");
            //int port = Convert.ToInt32(Console.ReadLine());
            //var server = new Dulo.Network.Server(port);
            //server.StartListening();
            //Console.ReadLine();
            ////var cl1 = new Client(port);

            ////cl1.OnConnectionDenied += Cl1_OnConnectionDenied;
            ////cl1.OnConnectionSuccess += Cl1_OnConnectionSuccess;
            ////cl1

            ////Console.WriteLine("write ip to send");
            ////string ip = Console.ReadLine();

            ////string mes;
            ////Console.WriteLine("write port to send");
            ////port = Convert.ToInt32(Console.ReadLine());

            ////while (true)
            ////{
            ////    Console.WriteLine("write message");
            ////    mes = Console.ReadLine();
            ////    cl1.Send("mes", new System.Net.IPEndPoint(IPAddress.Parse(ip), port));
            ////}
        }

        private static void Cl1_OnConnectionSuccess()
        {
            Console.WriteLine("Ok");
        }

        private static void Cl1_OnConnectionDenied()
        {
            Console.WriteLine("Lexa");
        }

        private static void Cl1_ReciveMessage(string message, System.Net.IPEndPoint ipEndPoint)
        {
            Console.WriteLine(message);
        }
    }
}
