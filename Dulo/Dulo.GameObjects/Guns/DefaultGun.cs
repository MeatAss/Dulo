using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.GameObjects.Guns
{
    public class DefaultGun : BaseGun
    {
        public DefaultGun(World world, Texture2D physicalTextureMapBullet) : base(world, physicalTextureMapBullet)
        {

        }
    }
}
