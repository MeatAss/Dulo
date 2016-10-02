using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;

namespace Dulo.BaseModels
{
    public class StaticModel : BaseModel
    {
        public StaticModel(World world, Texture2D physicalTextureMap) : base(world, physicalTextureMap)
        {
        }
    }
}
