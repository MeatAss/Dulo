using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public static class ExtensionIPEndPoint
    {
        public static IPEndPoint Clone(this IPEndPoint ipEndPoint)
        {
            return new IPEndPoint(new IPAddress(ipEndPoint.Address.Address), ipEndPoint.Port);
        }
    }
}
