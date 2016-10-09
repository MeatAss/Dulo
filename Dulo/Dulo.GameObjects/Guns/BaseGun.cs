using Dulo.BaseModels;
using Dulo.BasisModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Dulo.GameObjects.SettingsModels;

namespace Dulo.GameObjects.Guns
{
    public abstract class BaseGun : BaseBasis
    {
        private List<Bullet> Bullets { get; } = new List<Bullet>();

        private long lastShotTime;

        private readonly SettingBullet settingBullet;


        public int CountBullets { get; protected set; } = 20;

        public float DelayShot { get; protected set; } = 100;

    
        protected BaseGun(SettingBullet settingBullet)
        {
            this.settingBullet = settingBullet;

            lastShotTime = DateTime.Now.ToMilliseconds();
        }

        public void Fire(Vector2 startPosition, float startDirection)
        {
            if (Bullets.Count >= CountBullets || DateTime.Now.ToMilliseconds() - lastShotTime < DelayShot)
                return;

            var bullet = CreateDefaultBullet(settingBullet, startPosition, startDirection);

            Bullets.Add(bullet);

            lastShotTime = DateTime.Now.ToMilliseconds();
        }

        private Bullet CreateDefaultBullet(SettingBullet settingBullet, Vector2 startPosition, float startDirection)
        {
            var bullet = new Bullet(settingBullet, startPosition, startDirection);
            bullet.OnBulletDeath += Bullet_OnBulletDeath;

            bullet.AnimationPlay();

            return bullet;
        }

        private void Bullet_OnBulletDeath(Bullet bullet)
        {
            //World.RemoveBody(bullet.Body);
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
