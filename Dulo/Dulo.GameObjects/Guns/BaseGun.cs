using Dulo.BaseModels;
using Dulo.BasisModels;
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
    public abstract class BaseGun : BaseBasis
    {
        private List<Bullet> Bullets { get; set; } = new List<Bullet>();

        protected float bulletSpeed = 0.5f;

        protected int countBullets { get; set; } = 20;

        protected float delayShot { get; set; } = 100;


        private World world;
        private Texture2D texture;

        private long lastShotTime;


        public Animation DefaultBulletAnimation { get; set; }

        public Animation ExplosionBulletAnimation { get; set; }

        public Animation EndingLifetimeBulletAnimation { get; set; }

        public Animation ColisionBulletAnimation { get; set; }


        public BaseGun(World world, Texture2D physicalTextureMapBullet)
        {
            this.world = world;
            texture = physicalTextureMapBullet;

            lastShotTime = DateTime.Now.ToMilliseconds();
        }

        public void Fire(Vector2 startPosition, float startDirection)
        {
            if (Bullets.Count >= countBullets || DateTime.Now.ToMilliseconds() - lastShotTime < delayShot)
                return;

            var bullet = CreateDefaultBullet(startPosition, startDirection);

            Bullets.Add(bullet);
            bullet.Fire(startPosition, startDirection, bulletSpeed);

            lastShotTime = DateTime.Now.ToMilliseconds();
        }

        private Bullet CreateDefaultBullet(Vector2 startPosition, float startDirection)
        {
            var bullet = new Bullet(world, texture);
            bullet.OnBulletDeath += Bullet_OnBulletDeath;

            if (DefaultBulletAnimation == null)
                throw new Exception("Initialize DefaultBulletAnimation!");

            bullet.AddNewAnimation(DefaultBulletAnimation, "default");
            bullet.ChangeAnimation("default");
            bullet.AnimationPlay();

            return bullet;
        }

        private void Bullet_OnBulletDeath(Bullet bullet)
        {
            world.RemoveBody(bullet.Body);
            Bullets.Remove(bullet);
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
