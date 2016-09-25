using Dulo.Dulo.Action;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.Models
{
    public class Bullet : Model
    {
        public Mover Mover;

        public Bullet(Texture2D texture) : base(texture)
        {
            Mover = new Mover(this);
        }

        public Bullet(Texture2D texture, Vector2 size) : base(texture, size)
        {
            Mover = new Mover(this);
        }

        public Bullet(Texture2D texture, Vector2 position, Vector2 size) : base(texture, position, size)
        {
            Mover = new Mover(this);
        }
    }
}
