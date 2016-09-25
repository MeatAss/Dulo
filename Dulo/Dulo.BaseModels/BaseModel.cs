using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dulo.BaseModels
{
    public abstract class BaseModel : BaseBasis
    {
        protected Texture2D texture;

        public Vector2 Position { get; set; }

        public float Scale { get; set; } = 1;

        public float Angle { get; set; }

        public override void Render(SpriteBatch canvas)
        {
            canvas.Draw(texture, Position, null, Color.White, Angle, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
