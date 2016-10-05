using Dulo.BaseModels;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.GameObjects.Guns
{
    public abstract class BaseGun : BaseModels.BaseBasis
    {
        private List<Bullet> Bullets { get; set; } = new List<Bullet>();

        public int CountBullets { get; set; } = 20;

        protected float bulletSpeed = 0.5f;

        public float DelayShot { get; set; } = 100;
        private long lastShotTime;

        private World world;
        private Texture2D texture;

        public BaseGun(World world, Texture2D physicalTextureMapBullet)
        {
            this.world = world;
            texture = physicalTextureMapBullet;

            lastShotTime = DateTime.Now.ToMilliseconds();
        }

        public void Fire(Vector2 startPosition, float startDirection)
        {
            if (CountBullets > Bullets.Count && DateTime.Now.ToMilliseconds() - lastShotTime > DelayShot)
            {
                var Bullet = new Bullet(world, texture);
                Bullet.OnBulletDeath += Bullet_OnBulletDeath;

                var animationBullet = new BaseModels.Animation();
                animationBullet.Frames.Add(texture);
                Bullet.AddNewAnimation(animationBullet, "fire");
                Bullet.ChangeAnimation("fire");
                Bullet.Fire(startPosition, startDirection, bulletSpeed);

                Bullets.Add(Bullet);

                lastShotTime = DateTime.Now.ToMilliseconds();
            }
        }

        private void Bullet_OnBulletDeath(Bullet bullet)
        {
            world.RemoveBody(bullet.Body);
            Bullets.Remove(bullet);
            bullet = null; 
        }

        public override void Draw(SpriteBatch canvas)
        {
            base.Draw(canvas);

            //Улучшить
            Bullets.ForEach((bulet) => bulet.Draw(canvas));
            //Bullets?.Draw(canvas);
        }

        public override void Update()
        {
            base.Update();

            //Улучшить
            Bullets.ForEach((bulet) => bulet.Update());
            //Bullets?.Update();
        }
    }
}
