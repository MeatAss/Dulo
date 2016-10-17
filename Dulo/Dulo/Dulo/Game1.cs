using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dulo.GameObjects;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using Dulo.InputModel;
using System.Collections.Generic;
using Dulo.InputModel.InputSystem.ConcreteInputSystems;
using Microsoft.Xna.Framework.Input;
using Dulo.InputModel.InputSystem;

namespace Dulo
{

    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;

        private World world;

        public Matrix SimProjection;
        public Matrix SimView;
        public Matrix View;
        private Tank tank;
        private FrameCounter fps;

        private KeyListener keyListener;

        private SpriteFont font;
        private Wall wall;

        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;                          
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("SpriteFont1");
            fps = new FrameCounter(this, spriteBatch, font);
            Components.Add(fps);

            InitialzeWorld();

            keyListener = KeyListener.Create(new KeyboardWithMouseInputSystem(GetKeyboardInputWithMouseSystemMap()));

            tank = new GameObjectBuilder(world, View, Content).CreateDefaultTank();

            wall = new GameObjectBuilder(world, View, Content).CreateWall();

            wall.Position = new Vector2(-122, -20);

            //LabyrinthMap.AlaxText();
        }

        private void InitialzeWorld()
        {
            View = Matrix.Identity;

            Matrix matRotation = Matrix.CreateRotationZ(0);
            Matrix matZoom = Matrix.CreateScale(2f);

            Vector3 translateCenter = new Vector3(new Vector2(ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Width / 2f), ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Height / 2f)), 0f);

            translateCenter = ConvertUnits.ToDisplayUnits(translateCenter);

            View = matRotation * matZoom * Matrix.CreateTranslation(translateCenter);

            world = new World(new Vector2(0, 0));
        }


        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            keyListener.Update();

            tank.Update();
            wall.Update();

            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(0, null, null, null, null, null, View);
            tank.Draw(spriteBatch);
            wall.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private List<KeyboardInputWithMouseSystemMap> GetKeyboardInputWithMouseSystemMap()
        {
            var keyMap = new List<KeyboardInputWithMouseSystemMap>();

            keyMap.Add(new KeyboardInputWithMouseSystemMap { KeyboardKey = Keys.A, Operation = GameOperation.TurnLeft });
            keyMap.Add(new KeyboardInputWithMouseSystemMap { KeyboardKey = Keys.D, Operation = GameOperation.TurnRight });
            keyMap.Add(new KeyboardInputWithMouseSystemMap { KeyboardKey = Keys.W, Operation = GameOperation.MoveUp });
            keyMap.Add(new KeyboardInputWithMouseSystemMap { KeyboardKey = Keys.S, Operation = GameOperation.MoveDown });
            keyMap.Add(new KeyboardInputWithMouseSystemMap { MouseKey = MouseKeys.LeftButton, Operation = GameOperation.Fire });

            return keyMap;
        }

        private List<KeyboardInputSystemMap> GetKeyboardInputSystemMap()
        {
            var keyMap = new List<KeyboardInputSystemMap>();

            keyMap.Add(new KeyboardInputSystemMap { Key = Keys.A, Operation = GameOperation.TurnLeft });
            keyMap.Add(new KeyboardInputSystemMap { Key = Keys.D, Operation = GameOperation.TurnRight });
            keyMap.Add(new KeyboardInputSystemMap { Key = Keys.W, Operation = GameOperation.MoveUp });
            keyMap.Add(new KeyboardInputSystemMap { Key = Keys.S, Operation = GameOperation.MoveDown });
            keyMap.Add(new KeyboardInputSystemMap { Key = Keys.LeftShift, Operation = GameOperation.Fire });
            keyMap.Add(new KeyboardInputSystemMap { Key = Keys.Left, Operation = GameOperation.RotateTurretLeft });
            keyMap.Add(new KeyboardInputSystemMap { Key = Keys.Right, Operation = GameOperation.RotateTurretRight });

            return keyMap;
        }
    }
}
