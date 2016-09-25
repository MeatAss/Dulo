using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.GameObjects.Guns
{
    public class AngelModel
    {
        private float angle;
        private int fourth;
        private float leftAngle;
        private float rightAngle;

        public int Fourth
        {
            get
            {
                return fourth;
            }
        }

        public float LeftAngle
        {
            get
            {
                return leftAngle;
            }
        }

        public float RightAngle
        {
            get
            {
                return rightAngle;
            }
        }

        public AngelModel(float angle)
        {
            this.angle = angle;

            fourth = Convert.ToInt32(Math.Truncate(angle / 90));
            leftAngle = angle - Fourth * 90;
            rightAngle = 90 - LeftAngle;
        }
    }
}
