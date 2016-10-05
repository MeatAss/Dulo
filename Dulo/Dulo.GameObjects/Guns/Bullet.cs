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
    public delegate void BulledDeath(Bullet bullet);

    public class Bullet : BaseAnimationModel
    {
        public event BulledDeath OnBulletDeath;

        private Mover mover;

        public bool IsMoving { get; set; } = false;

        public float DeathTime { get; set; } = 5000;
        private long beginTime;

        public Bullet(World world, Texture2D physicalTextureMap) : base(world, physicalTextureMap)
        {
            mover = new Mover(this, 0, 0);
            Body.Restitution = 1;
        }

        public override void Update()
        {
            base.Update();
            
            if (DateTime.Now.ToMilliseconds() - beginTime > DeathTime)
                OnBulletDeath?.Invoke(this);
        }

        public override void Draw(SpriteBatch canvas)
        {
            base.Draw(canvas);
        }

        public void Fire(Vector2 startPosition, float startDirection, float speed)
        {
            Body.Position = startPosition;
            Body.Rotation = startDirection;
            mover.MoveTo(speed);

            IsMoving = true;
            beginTime = DateTime.Now.ToMilliseconds();
        }
    }
}
