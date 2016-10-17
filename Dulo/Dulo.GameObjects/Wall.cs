using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Dulo.GameObjects.SettingsModels;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.GameObjects
{
    public class Wall : BaseAnimationModel
    {
        public Wall(SettingWall setting) : base(setting)
        {
            Body.IsKinematic = true;
        }
    }
}
