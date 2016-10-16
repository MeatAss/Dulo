using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.InputModel.InputSystem;

namespace Dulo.InputModel
{
    public class ManagerGameOperationAction
    {
        private readonly List<GameOperationAction> conditionActionList;

        public ManagerGameOperationAction()
        {
            conditionActionList = new List<GameOperationAction>();
        }

        public void Add(GameOperation bindKey, Action action)
        {
            conditionActionList.Add(new GameOperationAction
            {
                gameOperation =bindKey,
                Action = action
            });
        }

        public void Check(GameOperation input)
        {
            conditionActionList
                .Where(cond => cond.gameOperation == input)
                .ToList()
                .ForEach(cond => cond.Action());
        }
    }
}
