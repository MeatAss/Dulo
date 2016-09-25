using Dulo.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.Dulo.Action
{
    public class Mover
    {
        public Model Model { get; set; }

        public Mover(Model model)
        {
            Model = model;
        }


        public void Update()
        {            
        }

        public void Rotate(float speed)
        {
            Model.Angle = Model.Angle > 2 * Math.PI ? 0 : Model.Angle + speed;
        }

        public void MoveTo(float speed)
        {
            float angle = Model.Angle - (float)Math.PI / 2;

            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

            direction.Normalize();
            Model.Position += direction * speed;
        }
    }
}
