using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Dulo.Models;
using Dulo.Dulo.Action;

namespace Dulo
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keysState;

        Tank tankLexa;
        KeyMap firstPlayerKeys;
        Texture2D a;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            
        }

        protected override void Initialize()
        {        
            firstPlayerKeys = new KeyMap() { Down = Keys.S, Fire = Keys.F, Left = Keys.A, Right = Keys.D, Up = Keys.W };

            tankLexa = new Tank(Content.Load<Texture2D>("tank"), new Vector2(30, 20), new Vector2(100, 100), Content.Load<Texture2D>("tank"));
            a = Content.Load<Texture2D>("tank");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            tankLexa.Update();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(a, new Vector2(100, 100), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
