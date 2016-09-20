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
        public static Dulo.Network.Server server = new Dulo.Network.Server(5002);

        static void Main(string[] args)
        {            
            server.MaxConnection = 5;
            server.StartListening();

            Task.Factory.StartNew(TaskCount);

            Console.ReadLine();
        }

        public static void TaskCount()
        {
            while (true)
            {
                Console.Title = Convert.ToString(server.clients.Count);
            }
        }
    }
}
