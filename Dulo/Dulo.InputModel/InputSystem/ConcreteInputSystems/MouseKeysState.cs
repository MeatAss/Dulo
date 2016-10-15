using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection;

namespace Dulo.InputModel.InputSystem.ConcreteInputSystems
{
    public enum MouseKeys
    {
        LeftButton,
        RightButton,
        MiddleButton,
        XButton1,
        XButton2
    }

    public static class MouseKeysState
    {
        public static bool IsKeyDown(MouseKeys mouseKey, MouseState mouseState)
        {
            var keyName = Enum.GetName(typeof(MouseKeys), mouseKey);
            return (ButtonState)typeof(MouseState).GetProperty(keyName).GetValue(mouseState, null) == ButtonState.Pressed;
        }
    }
}
