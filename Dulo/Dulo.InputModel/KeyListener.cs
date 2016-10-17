using Dulo.BasisModels;
using Dulo.InputModel.InputSystem;
using System.Collections.Generic;
using System.Linq;

namespace Dulo.InputModel
{
    public delegate void InputAction(GameOperation gameOperation);

    public class KeyListener : BaseBasis
    {
        public event InputAction OnKeyDown;
        public event InputAction OnKeyUp;

        private List<GameOperation> currentKeysState;

        public static KeyListener Sender { get; private set; }

        public IInput Input { get; set; }   
          

        private KeyListener(IInput input)
        {
            Input = input;

            currentKeysState = input.GetState().GetPressedKeys().ToList();
        }

        public static KeyListener Create(IInput input)
        {
            Sender = Sender ?? new KeyListener(input);

            return Sender;
        }

        public override void Update()
        {
            var newState = Input.GetState().GetPressedKeys().ToList();

            InvokeOnKeyDown(newState);
            InvokeOnKeyUp(newState);

            currentKeysState = newState;
        }

        private void InvokeOnKeyDown(List<GameOperation> gameOperation)
        {
            foreach (var state in gameOperation)
            {
                OnKeyDown?.Invoke(state);
            }
        }

        private void InvokeOnKeyUp(List<GameOperation> newState)
        {
            if (newState.Count == 0)
                 currentKeysState.ToList().ForEach(x => OnKeyUp?.Invoke(x));
            
            currentKeysState
                .ForEach(state =>
                {
                    if (newState.Contains(state) == false)
                        OnKeyUp?.Invoke(state);
                });
        }
    }
}
