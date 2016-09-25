using Dulo.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dulo.GameObjects
{
    public class Tank : IRenderer, IUpdater
    {
        public Vector2 Position;

        public float Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
                tankHead.Angle = value;
            }
        }


        private float angle;

        private Animation tankBody;

        private StaticModel tankHead;


        public Tank()
        {

        }

        public void Render(SpriteBatch canvas)
        {
            canvas.Draw(tankBody.CurrentFrame, Position, null, Color.White, Angle, Size, 1, SpriteEffects.None, 0);

        }

        public void Update()
        {
            
        }
    }
}
