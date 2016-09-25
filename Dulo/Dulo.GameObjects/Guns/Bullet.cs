using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Dulo.Actions;
using Microsoft.Xna.Framework;

namespace Dulo.GameObjects.Guns
{
    public class Bullet : BaseAnimationModel
    {
        private Mover mover;

        public float Speed { get; set; }

        public Bullet(float speed)
        {
            mover = new Mover(this);

            Speed = speed;
        }

        public override void Update()
        {
            base.Update();

            mover.MoveTo(Speed);
        }

        public void Reflect(Rectangle rect)
        {
            var angelModel = new AngelModel(Angle);

            if (rect.Left <= Rect.Center.X && Rect.Center.X <= rect.Right)
                ReflectHorizontal(angelModel);

            if (rect.Top <= Rect.Center.Y && Rect.Center.Y <= rect.Bottom)
                ReflectVertical(angelModel);
        }

        private void ReflectVertical(AngelModel angelModel)
        {
            if (angelModel.Fourth == 0 || angelModel.Fourth == 2)
                Angle += angelModel.RightAngle * 2;

            if(angelModel.Fourth == 1 || angelModel.Fourth == 3)
                Angle -= angelModel.LeftAngle * 2;
        }

        private void ReflectHorizontal(AngelModel angelModel)
        {
            if (angelModel.Fourth == 0 || angelModel.Fourth == 2)
                Angle -= angelModel.LeftAngle * 2;

            if (angelModel.Fourth == 1 || angelModel.Fourth == 3)
                Angle += angelModel.RightAngle * 2;
        }
    }
}
