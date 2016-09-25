using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.InputModel
{
    public class ConditionAction
    {
        public Func<bool> Condition;

        public Action Action;
    }
}
