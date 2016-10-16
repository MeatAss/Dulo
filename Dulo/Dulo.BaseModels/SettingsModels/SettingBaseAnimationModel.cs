using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.BaseModels.SettingsModels
{
    public class SettingBaseAnimationModel : SettingBaseModel
    {
        public Animation DefaultAnimation { get; private set; }

        public SettingBaseAnimationModel(World world, Texture2D physicalTextureMap, Animation defaultAnimation) :  base(world, physicalTextureMap)
        {
            DefaultAnimation = defaultAnimation;
        }
    }
}
