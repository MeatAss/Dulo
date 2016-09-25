using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dulo.InputModel;

namespace Dulo.GameObjects
{
    public class Turret : StaticModel
    {
        public Turret(Texture2D texture) : base(texture)
        {

        }

        public Turret(Texture2D texture, Vector2 position) : base(texture, position)
        {

        }

        public void Fire()
        {

        }
    }
}
