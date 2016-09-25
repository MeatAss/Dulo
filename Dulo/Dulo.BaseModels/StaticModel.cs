using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BaseModels
{
    public class StaticModel : BaseModel
    {
        public StaticModel()
        {

        }

        public StaticModel(Texture2D texture)
        {
            base.texture = texture;
        }

        public StaticModel(Texture2D texture, Vector2 position)
        {
            base.texture = texture;
            Position = position;
        }
    }
}
