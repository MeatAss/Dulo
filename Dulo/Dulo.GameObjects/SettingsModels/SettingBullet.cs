using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.GameObjects.SettingsModels
{
    public class SettingBullet : SettingBaseAnimationModel
    {
        public float Speed { get; private set; }

        public Animation ExplosionAnimation { get; private set; }

        public Animation EndingLifetimeAnimation { get; private set; }

        public Animation ColisionAnimation { get; private set; }

        public SettingBullet(World world, Texture2D physicalTextureMap, float speed, 
            Animation defaultAnimation, Animation explosionAnimation, Animation endingLifetimeAnimation, Animation colisionAnimation) 
                : base(world, physicalTextureMap, defaultAnimation)
        {
            Speed = speed;
            ExplosionAnimation = explosionAnimation;
            EndingLifetimeAnimation = endingLifetimeAnimation;
            ColisionAnimation = colisionAnimation;
        }
    }
}
