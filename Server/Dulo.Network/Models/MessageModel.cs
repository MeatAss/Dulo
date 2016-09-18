using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network.Models
{
    public class MessageModel
    {
        public byte Head
        {
            get;
            set;
        }

        public string Body
        {
            get;
            set;
        }
    }
}
