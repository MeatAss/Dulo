using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.InputModel.InputSystem.ConcreteInputSystems
{
    public class KeyboardWithMouseInputSystem : IInput
    {
        private readonly IEnumerable<KeyboardInputWithMouseSystemMap> map;

        public KeyboardWithMouseInputSystem(IEnumerable<KeyboardInputWithMouseSystemMap> keyMap)
        {
            map = keyMap;
        }

        public InputState GetState()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            var gameOperations = map
                .Where(x => IsKeyDown(x.GetKey(), keyboardState, mouseState))
                .Select(x => x.Operation)
                .ToList();

            return new InputState(gameOperations);
        }

        private bool IsKeyDown(EnumValueModel enumValue, KeyboardState keyboardState, MouseState mouseState)
        {
            if (enumValue.EnumType == typeof(Keys))
                return keyboardState.IsKeyDown((Keys)enumValue.EnumValue);

            return MouseKeysState.IsKeyDown((MouseKeys)enumValue.EnumValue, mouseState);
        }
    }
}
