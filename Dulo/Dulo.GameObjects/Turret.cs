using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dulo.BaseModels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dulo.InputModel;
using FarseerPhysics.Dynamics;

namespace Dulo.GameObjects
{
    public class Turret : BaseAnimationModel
    {

        public Turret(World world, Texture2D physicalTextureMap) : base(world, physicalTextureMap)
        {

        }
    }
}
