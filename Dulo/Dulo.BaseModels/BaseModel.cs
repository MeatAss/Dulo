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

        private Rectangle rect;
        public Rectangle Rect
        {
            get
            {
                return rect;
            }
        }

        public Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                rect.Location = new Point((int)position.X, (int)position.Y);
            }
        }

        private float scale = 1f;
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                if (value <= 0)
                {
                    scale = 1;
                }
                else
                {
                    scale = value;
                }

                rect.Width = (int)Math.Round(texture.Width * scale);
                rect.Height = (int)Math.Round(texture.Height * scale);
            }
        }

        public float Angle { get; set; }

        public override void Render(SpriteBatch canvas)
        {
            canvas.Draw(texture, Position, null, Color.White, Angle, new Vector2(rect.Center.X, rect.Center.Y), Scale, SpriteEffects.None, 0);
        }
    }
}
