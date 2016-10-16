using Dulo.BasisModels;
using Dulo.InputModel.InputSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.InputModel
{
    public delegate void InputAction(GameOperation gameOperation);

    public class KeyListener : BaseBasis
    {
        public event InputAction OnKeyDown;
        public event InputAction OnKeyUp;

        private IEnumerable<GameOperation> currentKeysState;

        private static KeyListener sender;

        public IInput Input { get; set; }   
        
        public static KeyListener Sender
        {
            get
            {
                return sender;
            }
        }     

        private KeyListener(IInput input)
        {
            Input = input;

            currentKeysState = input.GetState().GetPressedKeys();
        }

        public static KeyListener Create(IInput input)
        {
            if (sender == null)
                sender = new KeyListener(input);

            return sender;
        }

        public override void Update()
        {
            var newState = Input.GetState().GetPressedKeys();

            InvokeOnKeyDown(newState);

            InvokeOnKeyUp(newState);

            currentKeysState = newState;
        }

        private void InvokeOnKeyDown(IEnumerable<GameOperation> gameOperation)
        {
            foreach (var state in gameOperation)
            {
                OnKeyDown?.Invoke(state);
            }
        }

        private void InvokeOnKeyUp(IEnumerable<GameOperation> gameOperation)
        {
            //currentKeysState
            //    .ToList()
            //    .ForEach(state =>
            //    {
            //        if (gameOperation.Contains(state) == false)
            //            OnKeyUp?.Invoke(state);
            //    });
            if (gameOperation.Count() == 0)
                 currentKeysState.ToList().ForEach(x => OnKeyUp?.Invoke(x));

            foreach (var state in currentKeysState)
            {
                if (gameOperation.Where(x => x == state).Count() == 0 )
                    OnKeyUp?.Invoke(state);
            }
        }
    }
}
