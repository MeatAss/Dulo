using System;
using Dulo.BaseModels;
using Dulo.GameObjects.SettingsModels;
using Microsoft.Xna.Framework;

namespace Dulo.GameObjects.Guns
{
    public delegate void BulledDeath(Bullet bullet);

    public class Bullet : BaseAnimationModel
    {
        public event BulledDeath OnBulletDeath;

        public float DeathTime { get; set; } = 5000;
        private long beginTime;

        private readonly SettingBullet settingBaseAnimationModel;

        public Bullet(SettingBullet settingBullet, Vector2 startPosition, float startDirection) : base(settingBullet)
        {
            settingBaseAnimationModel = settingBullet;

            Body.Restitution = 1;
            Fire(startPosition, startDirection, settingBullet.Speed);
        }

        public override void Update()
        {
            base.Update();
            
            if (DateTime.Now.ToMilliseconds() - beginTime > DeathTime)
                OnBulletDeath?.Invoke(this);
        }

        private void Fire(Vector2 startPosition, float startDirection, float speed)
        {
            Body.Position = startPosition;
            Body.Rotation = startDirection;
            MoveTo(speed);

            beginTime = DateTime.Now.ToMilliseconds();
        }
    }
}
