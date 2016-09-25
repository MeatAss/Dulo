﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.Dulo.Action
{
    public class KeyMap : IMovingKeyMap, IShootingKeyMap
    {
        public Keys Up { get; set; }

        public Keys Down { get; set; }

        public Keys Left { get; set; }

        public Keys Right { get; set; }

        public Keys Fire { get; set; }
    }
}
