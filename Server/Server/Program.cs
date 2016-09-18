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
            var server = new Dulo.Network.Server(5002);
            server.MaxConnection = 5;
            server.StartListening();

            Console.ReadLine();
        }
    }
}
