using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BaseModels
{
    public interface IRenderer
    {
        void Draw(SpriteBatch canvas);
    }
}
