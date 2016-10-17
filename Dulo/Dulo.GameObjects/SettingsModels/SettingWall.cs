using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.GameObjects.SettingsModels
{
    public class SettingWall : SettingBaseAnimationModel
    {
        public SettingWall(World world, Texture2D physicalTextureMap, Animation defaultAnimation) : base(world, physicalTextureMap, defaultAnimation)
        {
        }
    }
}
