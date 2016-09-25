using Dulo.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dulo.InputModel;
using Dulo.Actions;
using Microsoft.Xna.Framework.Input;

namespace Dulo.GameObjects
{
    public class Tank : BaseAnimationModel 
    {
        private IMovingKeyMap movingKeyMap;
        private IShootingKeyMap shootingKeyMap;
        private KeyListener keyListener;

        private Mover mover;

        private float speedMoving;
        private float speedRotating;

        public float SpeedMoving
        {
            get
            {
                return speedMoving;
            }
            set
            {
                speedMoving = value < 0 ? 0 : value;
            }
        }

        public float SpeedRotating
        {
            get
            {
                return speedRotating;
            }
            set
            {
                speedRotating = value < 0 ? 0 : value;
            }
        }

        public Tank(KeyMap keyMap)
        {
            movingKeyMap = keyMap;
            shootingKeyMap = keyMap;

            mover = new Mover(this);
            InitializeKeyListener();
        }

        private void InitializeKeyListener()
        {
            keyListener = new KeyListener();
            keyListener.Add(movingKeyMap.Up, () => mover.MoveTo(speedMoving));
            keyListener.Add(movingKeyMap.Down, () => mover.MoveTo(-speedMoving * 0.6f));
            keyListener.Add(movingKeyMap.Left, () => mover.Rotate(-speedRotating));
            keyListener.Add(movingKeyMap.Right, () => mover.Rotate(speedRotating));
        }

        public override void Render(SpriteBatch canvas)
        {

            base.Render(canvas);
        }

        public override void Update()
        {
            base.Update();

            keyListener.Check(Keyboard.GetState());            
        }        
    }
}
