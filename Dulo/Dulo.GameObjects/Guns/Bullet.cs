using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Dulo.Actions;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.GameObjects.Guns
{
    public class Bullet : BaseAnimationModel
    {
        private Mover mover;

        public float Speed { get; set; }


        public Bullet(World world, Texture2D physicalTextureMap, float speed) : base(world, physicalTextureMap)
        {
            mover = new Mover(this, 0, 0);

            Speed = speed;
        }

        public override void Update()
        {
            base.Update();

            mover.MoveTo(Speed);
        }
    }
}
