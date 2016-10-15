using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Dulo.InputModel.InputSystem.ConcreteInputSystems
{
    public class KeyboardInputSystem : IInput
    {
        private readonly IEnumerable<KeyboardInputSystemMap> map;

        public KeyboardInputSystem(IEnumerable<KeyboardInputSystemMap> keyMap)
        {
            map = keyMap;
        }

        public InputState GetState()
        {
            var keyboardState = Keyboard.GetState();

            var gameOperations = map
                .Where(x => keyboardState.IsKeyDown(x.Key))
                .Select(x => x.Operation)
                .ToList();

            return new InputState(gameOperations);
        }
    }
}
