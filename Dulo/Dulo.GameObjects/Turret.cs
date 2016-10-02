using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Dulo.InputModel;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;

namespace Dulo.GameObjects
{
    public class Turret : BaseAnimationModel
    {
        public float test;

        public Turret(World world, Texture2D physicalTextureMap) : base(world, physicalTextureMap)
        {
            Body.AngularDamping = 50f;
        }

        public override void Update()
        {
            base.Update();

            var mouseState = Mouse.GetState();
            var bodyRot = new Vector2((float)Math.Cos(Position.X), (float)Math.Sin(Position.Y));

            var Cursor = new Vector2(mouseState.X, mouseState.Y);

            var mouseVector = new Vector2(mouseState.X - Position.X, mouseState.Y - Position.Y);

            //angle = (float)Math.Atan2(mouseVector.Y, mouseVector.X) + MathHelper.PiOver2;

            test = (float)Math.Acos((bodyRot.X * mouseVector.X + bodyRot.Y * mouseVector.Y) / (bodyRot.Length() * mouseVector.Length())) - MathHelper.PiOver2;

            //if (angle - Body.Rotation <= -0.03f)
            //{
            //    Body.ApplyTorque(-0.5f);
            //}

            //if (angle - Body.Rotation >= 0.03f)
            //{
            //    Body.ApplyTorque(0.5f);
            //}
        }
    }
}
