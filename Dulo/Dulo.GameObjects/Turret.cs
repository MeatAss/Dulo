﻿using System;
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
using System.IO;
using Dulo.GameObjects.Guns;

namespace Dulo.GameObjects
{
    public class Turret : BaseAnimationModel
    {
        public bool IsLookAtMouse { get; set; } = true;

        public float SpeedRotation { get; set; } = 0.5f;

        private float percentageError = 0.034f;

        public DefaultGun gun;

        public Turret(World world, Texture2D physicalTextureMap, Texture2D physicalTextureMapBullet) : base(world, physicalTextureMap)
        {
            Body.AngularDamping = 50f;

            gun = new DefaultGun(world, physicalTextureMapBullet);
        }

        public override void Update()
        {
            base.Update();
            
            if (!IsLookAtMouse)
                return;

            var mouseAngle = GetMouseAngle();

            if (Math.Abs(mouseAngle - Angle) > percentageError)
                RotateTurretToCursor(mouseAngle);             

            gun.Update();
        }

        private float GetMouseAngle()
        {
            var mouseState = Mouse.GetState();

            
            var angle = (float)Math.Atan2((mouseState.Y - Position.Y), (mouseState.X - Position.X)) + MathHelper.PiOver2;

            angle = angle >= 0 ? MathHelper.TwoPi - angle : Math.Abs(angle);
            angle = MathHelper.TwoPi - angle;

            return angle;
        }

        private void RotateTurretToCursor(float mouseAngle)
        {
            if (mouseAngle - Angle < 0)
            {
                var direction = MathHelper.TwoPi + mouseAngle - Angle >= MathHelper.Pi ? -1 : 1;

                Body.ApplyTorque(SpeedRotation * direction);
            }
            else
            {
                var direction = mouseAngle - Angle > MathHelper.Pi ? -1 : 1;

                Body.ApplyTorque(SpeedRotation * direction);
            }
        }

        public void Fire()
        {
            gun.Fire(Body.Position + GetDirection() * 0.5f, Body.Rotation);
        }

        public override void Draw(SpriteBatch canvas)
        {
            base.Draw(canvas);

            gun.Draw(canvas);
        }
    }
}
