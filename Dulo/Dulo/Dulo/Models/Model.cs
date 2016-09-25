using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.Models
{
    public abstract class Model : IModel
    {
        private float angle;

        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public Texture2D Texture { get; set; }

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
            }
        }

        public Model(Texture2D texture)
        {
            Texture = texture;
        }

        public Model(Texture2D texture, Vector2 size)
        {
            Texture = texture;
            Size = size;
        }

        public Model(Texture2D texture, Vector2 position, Vector2 size)
        {
            Texture = texture;
            Size = size;
            Position = position;
        }

        void IModel.Render(SpriteBatch canvas)
        {
            canvas.Begin();
    
            canvas.End();
        }
    }
}
