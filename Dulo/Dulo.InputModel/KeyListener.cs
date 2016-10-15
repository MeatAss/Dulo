using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.InputModel.InputSystem;

namespace Dulo.InputModel
{
    public class KeyListener
    {
        private readonly List<ConditionAction> conditionActionList;

        private InputState inputState;


        public KeyListener()
        {
            conditionActionList = new List<ConditionAction>();
        }


        public void Add(GameOperation bindKey, Action action)
        {
            conditionActionList.Add(new ConditionAction
            {
                Condition = () => inputState.IsKeyDown(bindKey),
                Action = action
            });
        }

        public void Check(InputState inputState)
        {
            this.inputState = inputState;

            conditionActionList
                .Where(cond => cond.Condition())
                .ToList()
                .ForEach(cond => cond.Action());
        }
    }
}
