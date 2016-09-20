using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public class ClientModel
    {
        public IPEndPoint ClientIp { get; private set; }

        public long ClientsLastMessageTime { get; set; }

        public ClientModel(IPEndPoint clientIp)
        {
            ClientIp = clientIp;
            UpdateTime();
        }

        public void UpdateTime()
        {
            ClientsLastMessageTime = DateTime.Now.ToMilliseconds();
        }
    }
}
