using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.Dulo.Action
{
    public class KeyListener
    {
        private List<ConditionAction> conditionActionList;

        private KeyboardState keysState;

        public KeyListener()
        {
            conditionActionList = new List<ConditionAction>();
        }

        public void Add(Keys bindKey, Action<object> action)
        {
            conditionActionList.Add(new ConditionAction
            {
                Condition = () => keysState.IsKeyDown(bindKey),
                Action = action
            });
        }

        public void Check(KeyboardState keysState)
        {
            this.keysState = keysState;

            conditionActionList.ForEach((item) =>
            {
                if (item.Condition())
                    item.Action(null);
            });
        }
    }
}
