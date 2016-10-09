using System;
using Dulo.BaseModels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dulo.BaseModels.SettingsModels;
using Dulo.GameObjects.Guns;

namespace Dulo.GameObjects
{
    public class Turret : BaseAnimationModel
    {
        public bool IsLookAtMouse { get; set; } = true;

        public float SpeedRotation { get; set; } = 0.5f;

        private float percentageError = 0.034f;

        private BaseGun gun;
        private readonly MouseProcessor mouseProcessor;

        public Turret(SettingBaseAnimationModel settingBaseAnimation, MouseProcessor mouseProcessor, BaseGun gun) : base(settingBaseAnimation)
        {
            this.gun = gun; 
            this.mouseProcessor = mouseProcessor;

            Body.Mass = 0.1f;
            Body.AngularDamping = 15f;
        }

        public override void Update()
        {
            base.Update();
            
            if (!IsLookAtMouse)
                return;

            var mouseAngle = mouseProcessor.GetMouseAngle(Position);

            if (Math.Abs(mouseAngle - Angle) > percentageError)
                RotateTurretToCursor(mouseAngle);

            gun.Update();
        }

        public override void Draw(SpriteBatch canvas)
        {
            base.Draw(canvas);

            gun.Draw(canvas);
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

        public void ChangeGun(BaseGun gun)
        {
            this.gun = gun;
        }
    }
}
