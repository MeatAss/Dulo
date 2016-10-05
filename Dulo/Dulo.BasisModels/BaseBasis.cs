using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.BasisModels
{
    public abstract class BaseBasis : IRenderer, IUpdater
    {
        public virtual void Draw(SpriteBatch canvas)
        {
            
        }

        public virtual void Update()
        {
            
        }
    }
}
