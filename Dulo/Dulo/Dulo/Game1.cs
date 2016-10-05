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
        KeyMap keyMap2;
        Tank tank;
        Tank tank1;
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

            InitialzeWorld();

            keyMap = new KeyMap();
            keyMap.Up = Keys.W;
            keyMap.Down = Keys.S;
            keyMap.Right = Keys.D;
            keyMap.Left = Keys.A;
            keyMap.Fire = Keys.LeftShift;

            keyMap2 = new KeyMap();
            keyMap2.Up = Keys.Up;
            keyMap2.Down = Keys.Down;
            keyMap2.Right = Keys.Right;
            keyMap2.Left = Keys.Left;
            keyMap2.Fire = Keys.RightShift;

            Animation turretAnimation = new Animation();
            Animation turretAnimation2 = new Animation();
            for (int i = 1; i < 4; i++)
            {
                turretAnimation.Frames.Add(Content.Load<Texture2D>($"gun1/{i}"));
                turretAnimation2.Frames.Add(Content.Load<Texture2D>($"gun1/{i}"));
            }

            turretAnimation.IsCyclicAnimation = true;
            turretAnimation.AnimationSpeed = 10;


            Animation animationTankBody = new Animation();
            Animation animationTankBody1 = new Animation();
            for (int i = 0; i < 14; i++)
            {
                animationTankBody.Frames.Add(Content.Load<Texture2D>($"tank/Tank{i}"));
                animationTankBody1.Frames.Add(Content.Load<Texture2D>($"tank/Tank{i}"));
            }

            animationTankBody.IsCyclicAnimation = true;
            animationTankBody.AnimationSpeed = 10;
            animationTankBody1.IsCyclicAnimation = true;
            animationTankBody1.AnimationSpeed = 10;

            tank = new Tank(world, Content.Load<Texture2D>("tank/Tank0"), Content.Load<Texture2D>("gun1/1"), Content.Load<Texture2D>("bullets/bullet"), keyMap);                    
                                           
            tank.AddNewAnimation(animationTankBody, "moving");
            tank.ChangeAnimation("moving");
            tank.AnimationPlay();

            tank.AddTurretAnimation(turretAnimation, "shot");
            tank.ChangeTurretAnimation("shot");
            tank.TurretAnimationPlay();

            tank.SpeedMoving = 15f;
            tank.SpeedRotating = 7f;
            tank.Position = new Vector2(200, 300); 

            tank1 = new Tank(world, Content.Load<Texture2D>("tank/Tank0"), Content.Load<Texture2D>("gun1/1"), Content.Load<Texture2D>("bullets/bullet"), keyMap2);          
            tank1.AddNewAnimation(animationTankBody1, "moving");
            tank1.ChangeAnimation("moving");
            tank1.AnimationPlay();

            tank1.AddTurretAnimation(turretAnimation2, "shot");
            tank1.ChangeTurretAnimation("shot");

            tank1.SpeedMoving = 75f;
            tank1.SpeedRotating = 7f;
            tank1.Body.Mass = 5;
            tank1.Position = new Vector2(600, 300);
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
            tank.Update();
            tank1.Update();

            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            base.Update(gameTime);

            //Window.Title = tank.turret.gun.Bullets.IsMoving.ToString() + tank1.turret.gun.Bullets.IsMoving.ToString();
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin(0, null, null, null, null, null, View);
            spriteBatch.Begin();
            tank.Draw(spriteBatch);
            tank1.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
