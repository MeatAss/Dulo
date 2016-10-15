using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.InputModel.InputSystem.ConcreteInputSystems
{
    public class EnumValueModel
    {
        public Type EnumType { get; }

        public int EnumValue { get; }

        public EnumValueModel(Type enumType, int enumValue)
        {
            EnumValue = enumValue;
            EnumType = enumType;
        }
    }
}
