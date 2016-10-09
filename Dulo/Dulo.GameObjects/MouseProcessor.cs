using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dulo.GameObjects
{
    public class MouseProcessor
    {
        private readonly Matrix view;

        public MouseProcessor(Matrix view)
        {
            this.view = view;
        }

        public float GetMouseAngle(Vector2 objectPosition)
        {
            var mouseState = Mouse.GetState();

            var relatedMousePosition = Vector2.Transform(new Vector2(mouseState.X, mouseState.Y), Matrix.Invert(view));

            var angle = (float)Math.Atan2(relatedMousePosition.Y - objectPosition.Y, relatedMousePosition.X - objectPosition.X) + MathHelper.PiOver2;

            angle = angle >= 0 ? MathHelper.TwoPi - angle : Math.Abs(angle);
            angle = MathHelper.TwoPi - angle;

            return angle;
        }
    }
}
