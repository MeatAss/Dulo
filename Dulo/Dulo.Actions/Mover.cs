using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Dulo.InputModel;
using Dulo.BaseModels;

namespace Dulo.Actions
{
    public class Mover : BaseBasis
    {
        private BaseModel baseModel;

        public Mover(BaseModel baseModel, float linearDamping, float angularDamping)
        {
            this.baseModel = baseModel;

            baseModel.Body.LinearDamping = linearDamping;
            baseModel.Body.AngularDamping = angularDamping;
        }

        public void Rotate(float speed)
        {
            baseModel.Body.ApplyTorque(speed);
        }

        public void MoveTo(float speed)
        {
            baseModel.Body.ApplyForce(baseModel.GetDirection() * speed);
        }
    }
}
