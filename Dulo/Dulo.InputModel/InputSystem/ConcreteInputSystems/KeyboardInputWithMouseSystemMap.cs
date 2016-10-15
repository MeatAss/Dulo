using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.InputModel.InputSystem.ConcreteInputSystems
{
    public class KeyboardInputWithMouseSystemMap
    {
        private string key;   

        public MouseKeys MouseKey
        {
            set
            {
                key = Enum.GetName(typeof(MouseKeys), value);
            }
        }

        public Keys KeyboardKey
        {
            set
            {
                key = Enum.GetName(typeof(Keys), value);
            }
        }

        public GameOperation Operation { get; set; }

        public EnumValueModel GetKey()
        {
            MouseKeys mouseKeys;
            if (Enum.TryParse(key, out mouseKeys))
                return new EnumValueModel(typeof(MouseKeys), (int)mouseKeys);

            return new EnumValueModel(typeof(Keys), (int)Enum.Parse(typeof(Keys), key));
        }  
    }
}
