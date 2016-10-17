using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.GameObjects.Labyrinth
{
    public class Stack
    {
        object[] stack = new object[0];

        public object this[int i]
        {
            get
            {
                return stack[i];
            }

            set
            {
                stack[i] = value;
            }
        }

        public int Length
        {
            get
            {
                return stack.Length;
            }
        }

        public object LastObj
        {
            get
            {
                return stack[stack.Length - 1];
            }
        }

        public object GetAndRemoveFirstItem()
        {
            if (stack.Length == 0) return null;

            object Result = stack[0];

            for (int i = 0; i < stack.Length - 1; i++)
                stack[i] = stack[i + 1];

            Array.Resize(ref stack, stack.Length - 1);

            return Result;
        }

        public void Add(object obj)
        {
            Array.Resize(ref stack, stack.Length + 1);

            for (int i = stack.Length - 1; i > 0; i--)
                stack[i] = stack[i - 1];

            stack[0] = obj;
        }


    }
}
