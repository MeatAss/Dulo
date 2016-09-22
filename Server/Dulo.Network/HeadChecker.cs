using Dulo.Network.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public delegate void HeadAction(MessageModel model, object arg);

    public class HeadChecker
    {
        private Dictionary<byte, HeadAction> headActions;

        public HeadChecker()
        {
            headActions = new Dictionary<byte, HeadAction>();
        }

        public void Add(byte head, HeadAction action)
        {
            headActions.Add(head, action);
        }

        public void Remove(byte head)
        {
            headActions.Remove(head);
        }

        public bool Check(MessageModel model, object arg)
        {
            var result = headActions.FirstOrDefault(keyValue => keyValue.Key == model.Head);

            result.Value?.Invoke(model, arg);

            return result.Value != null;
        }
    }
}
