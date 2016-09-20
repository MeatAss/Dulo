using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public static class ExtensionDateTime
    {
        public static long ToMilliseconds(this DateTime dateTime)
        {
            return dateTime.Ticks / 10000;
        }
    }
}
