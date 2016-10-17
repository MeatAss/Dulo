using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.GameObjects
{
    public class TankCollider : BaseModel
    {
        public TankCollider(World world, Texture2D physicalTextureMap) : base(world, physicalTextureMap)
        {
        }

        public TankCollider(SettingBaseModel settingBaseModel) : base(settingBaseModel)
        {
        }

        public override void Draw(SpriteBatch canvas)
        {
            //base.Draw(canvas);
        }
    }
}
