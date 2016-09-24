using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public class BaseHeaders
    {
        private BaseHeaders()
        {

        }

        public static byte Connect
        {
            get
            {
                return 0;
            }
        }        

        public static byte ConnectionSuccess
        {
            get
            {
                return 1;
            }
        }

        public static byte ConnectionDenied
        {
            get
            {
                return 2;
            }
        }

        //public static byte Ping
        //{
        //    get
        //    {
        //        return 3;
        //    }
        //}

        public static byte Message
        {
            get
            {
                return 3;
            }
        }        
    }
}
