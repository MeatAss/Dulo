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
using Dulo.InputModel;

namespace Dulo
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyMap keyMap;
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

            keyMap = new KeyMap();
            keyMap.Up = Keys.W;
            keyMap.Down = Keys.S;
            keyMap.Right = Keys.D;
            keyMap.Left = Keys.A;

            tankLexa = new Tank(keyMap);
            tankLexa.Position = new Vector2(200, 300);
            
            Animation animationTankBody = new Animation();
            animationTankBody.Frames.Add(Content.Load<Texture2D>("1"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("2"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("3"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("4"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("5"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("6"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("7"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("8"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("9"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("10"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("11"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("12"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("13"));
            animationTankBody.Frames.Add(Content.Load<Texture2D>("14"));
          
            animationTankBody.IsCyclicAnimation = true;
            animationTankBody.AnimationSpeed = 42;

            tankLexa.AddNewAnimation(animationTankBody, "gogogo");
            tankLexa.ChangeAnimation("gogogo");
            tankLexa.AnimationPlay();

            tankLexa.SpeedMoving = 3f;
            tankLexa.SpeedRotating = 0.03f;


            
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
