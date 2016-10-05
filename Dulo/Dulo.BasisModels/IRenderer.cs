using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BasisModels
{
    public interface IRenderer
    {
        void Draw(SpriteBatch canvas);
    }
}
