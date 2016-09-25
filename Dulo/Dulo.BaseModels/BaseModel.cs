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

        public float Scale { get; set; } = 0.5f;

        public float Angle { get; set; }

        public override void Render(SpriteBatch canvas)
        {
            canvas.Draw(texture, Position, null, Color.White, Angle, new Vector2(texture.Width / 2, texture.Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
