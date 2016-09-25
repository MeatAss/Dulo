using Dulo.Dulo.Action;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.Models
{
    public class Tank : Model
    {
        private KeyMap keyMap { get; set; }

        public KeyMap KeyMap
        {
            set
            {
                keyMap = value;
                InitializeKeyListener();
            }
        }

        private KeyListener listener;

        private Mover mover;

        private Shooter shooter;

        private Texture2D textureBullet;


        public Tank(Texture2D texture, Texture2D textureBullet) : base(texture)
        {
            Initialize(textureBullet);
        }

        public Tank(Texture2D texture, Vector2 size, Texture2D textureBullet) : base(texture, size)
        {
            Initialize(textureBullet);
        }

        public Tank(Texture2D texture, Vector2 size, Vector2 position, Texture2D textureBullet) : base(texture, position, size)
        {
            Initialize(textureBullet);
        }

        private void Initialize(Texture2D textureBullet)
        {
            listener = new KeyListener();
            mover = new Mover(this);
            shooter = new Shooter(this, textureBullet, new Vector2(10, 10));

            this.textureBullet = textureBullet;

            keyMap = new KeyMap()
            {
                Down = Microsoft.Xna.Framework.Input.Keys.S,
                Up = Microsoft.Xna.Framework.Input.Keys.W,
                Left = Microsoft.Xna.Framework.Input.Keys.A,
                Right = Microsoft.Xna.Framework.Input.Keys.D,
                Fire = Microsoft.Xna.Framework.Input.Keys.Space 
            };

            InitializeKeyListener();
        }

        public void InitializeKeyListener()
        {
            listener.Add(keyMap.Right, (args) => mover.Rotate(0.05f));
            listener.Add(keyMap.Left, (args) => mover.Rotate(-0.05f));
            listener.Add(keyMap.Up, (args) => mover.MoveTo(3));
            listener.Add(keyMap.Down, (args) => mover.MoveTo(-3));
            listener.Add(keyMap.Fire, (args) => shooter.Fire());
        }

        public void Update()
        {
            listener.Check(Keyboard.GetState());
            shooter.Update();
        }

        public void SRender(SpriteBatch canvas)
        {
            shooter.Render(canvas);
        }
    }
}
