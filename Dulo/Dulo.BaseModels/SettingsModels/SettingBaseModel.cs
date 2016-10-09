using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.BaseModels.SettingsModels
{
    public class SettingBaseModel
    {
        public World World { get; private set; }

        public Texture2D PhysicalTextureMap { get; private set; }

        public SettingBaseModel(World world, Texture2D physicalTextureMap)
        {
            World = world;
            PhysicalTextureMap = physicalTextureMap;
        }
    }
}
