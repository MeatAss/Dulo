using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dulo.BaseModels
{
    public abstract class BaseModel : IRenderer
    {
        protected Texture2D texture;

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public float Angle { get; set; }


        public void Render(SpriteBatch canvas)
        {
            canvas.Draw(texture, Position, null, Color.White, Angle, Size, 1, SpriteEffects.None, 0);
        }
    }
}
