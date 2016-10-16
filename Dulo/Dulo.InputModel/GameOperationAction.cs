using Dulo.InputModel.InputSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.InputModel
{
    public class GameOperationAction
    {
        public GameOperation gameOperation { get; set; }

        public Action Action { get; set; }
    }
}
