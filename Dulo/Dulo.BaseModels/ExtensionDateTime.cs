using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BaseModels
{
    public static class ExtensionDateTime
    {
        public static long ToMilliseconds(this DateTime dateTime)
        {
            return dateTime.Ticks / 10000;
        }
    }
}
