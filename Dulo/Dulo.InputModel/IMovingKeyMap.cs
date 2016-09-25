using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.InputModel
{
    public interface IMovingKeyMap
    {
        Keys Up { get; set; }

        Keys Down { get; set; }

        Keys Left { get; set; }

        Keys Right { get; set; }
    }
}
