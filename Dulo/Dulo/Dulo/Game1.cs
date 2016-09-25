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
using Dulo.GameObjects;
using Dulo.BaseModels;

namespace Dulo
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Tank tankLexa;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;            
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tankLexa = new Tank();

            Animation animationTankBody = new Animation();
            animationTankBody.Frames.Add(Content.Load<Texture2D>("1"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("2"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("3"));
            animationTankBody.IsCyclicAnimation = true;
            animationTankBody.AnimationSpeed = 100;

            tankLexa.AddNewAnimation(animationTankBody, "gogogo");
            tankLexa.ChangeAnimation("gogogo");
            tankLexa.AnimationPlay();
        }

        protected override void UnloadContent()
        {

        }

        
        protected override void Update(GameTime gameTime)
        {
            tankLexa.Update();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            tankLexa.Render(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
