using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.BaseModels
{
    public abstract class BaseBasis : IRenderer, IUpdater
    {
        public virtual void Render(SpriteBatch canvas)
        {
            
        }

        public virtual void Update()
        {
            
        }
    }
}
