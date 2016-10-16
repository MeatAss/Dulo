using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Dulo.InputModel.InputSystem
{
    public enum GameOperation
    {
        MoveUp,
        MoveDown,
        TurnLeft,
        TurnRight,
        RotateTurretLeft,
        RotateTurretRight,
        Fire
    }

    public class InputState
    {
        private readonly List<GameOperation> keysPressed;

        public InputState(List<GameOperation> keysPressed)
        {
            this.keysPressed = keysPressed;
        }

        public bool IsKeyDown(GameOperation gameOperation)
        {
            return keysPressed.Contains(gameOperation);
        }

        public IEnumerable<GameOperation> GetPressedKeys()
        {
            return keysPressed;
        }
    }
}