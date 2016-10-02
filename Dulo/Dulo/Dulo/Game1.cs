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
using System.Threading;
using FarseerPhysics.Dynamics;
using FarseerPhysics;

namespace Dulo
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        World world;

        public Matrix SimProjection;
        public Matrix SimView;
        public Matrix View;


        KeyMap keyMap;
        KeyMap a;
        Tank tankLexa;
        Tank tankSlava;
        FrameCounter fps;

        private SpriteFont font;

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

            font = Content.Load<SpriteFont>("SpriteFont1");
            fps = new FrameCounter(this, spriteBatch, font);
            Components.Add(fps);



            keyMap = new KeyMap();
            keyMap.Up = Keys.W;
            keyMap.Down = Keys.S;
            keyMap.Right = Keys.D;
            keyMap.Left = Keys.A;

            InitialzeWorld();



            tankLexa = new Tank(world, Content.Load<Texture2D>("танк/Tank0"), Content.Load<Texture2D>("gun1/1"), keyMap);

            Animation a = new Animation();

            for (int i = 1; i < 4; i++)
                a.Frames.Add(Content.Load<Texture2D>($"gun1/{i}"));

            a.IsCyclicAnimation = true;
            a.AnimationSpeed = 10;

            tankLexa.AddTurretAnimation(a, "firefirefire");
            tankLexa.ChangeTurretAnimation("firefirefire");
            tankLexa.TurretAnimationPlay();

            tankLexa.Position = Vector2.Zero;


            Animation animationTankBody = new Animation();

            for (int i = 0; i < 14; i++)
                animationTankBody.Frames.Add(Content.Load<Texture2D>($"танк/Tank{i}"));

            
            animationTankBody.IsCyclicAnimation = true;
            animationTankBody.AnimationSpeed = 1;

            tankLexa.AddNewAnimation(animationTankBody, "gogogo");
            tankLexa.ChangeAnimation("gogogo");
            tankLexa.AnimationPlay();

            tankLexa.SpeedMoving = 15f;
            tankLexa.SpeedRotating = 7f;

            //a = new KeyMap();
            //a.Up = Keys.Up;
            //a.Down = Keys.Down;
            //a.Right = Keys.Right;
            //a.Left = Keys.Left;

            //tankSlava = new Tank(world, Content.Load<Texture2D>("танк/Tank0"), a);
            //tankSlava.Position = new Vector2(-100, -100);

            //tankSlava.AddNewAnimation(animationTankBody, "gogogo");
            //tankSlava.ChangeAnimation("gogogo");
            //tankSlava.AnimationPlay();
            //tankSlava.Position = new Vector2(-100, -100);

            //tankSlava.SpeedMoving = 15f;
            //tankSlava.SpeedRotating = 7f;

            //tankSlava.Body.Mass = 5;
        }


        private void InitialzeWorld()
        {
            SimProjection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Width),
                ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Height), 0f, 0f, 1f);

            SimView = Matrix.Identity;
            View = Matrix.Identity;

            Matrix matRotation = Matrix.CreateRotationZ(0);
            Matrix matZoom = Matrix.CreateScale(1);

            Vector3 translateCenter = new Vector3(new Vector2(ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Width / 2f), ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Height / 2f)), 0f);

            SimView = matRotation * matZoom * Matrix.CreateTranslation(translateCenter);

            translateCenter = ConvertUnits.ToDisplayUnits(translateCenter);

            View = matRotation * matZoom * Matrix.CreateTranslation(translateCenter);

            world = new World(new Vector2(0, 0));
        }


        protected override void UnloadContent()
        {

        }

        
        protected override void Update(GameTime gameTime)
        {
            
            tankLexa.Update();
            //tankSlava.Update();

            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            base.Update(gameTime);

            Window.Title = tankLexa.Position.ToString();
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
    
            spriteBatch.Begin(0, null, null, null, null, null, View);
            tankLexa.Draw(spriteBatch);
            //tankSlava.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
