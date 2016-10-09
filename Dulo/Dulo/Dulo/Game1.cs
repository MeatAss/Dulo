using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Dulo.GameObjects;
using FarseerPhysics.Dynamics;
using FarseerPhysics;

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

        private SpriteFont font;

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

            tank = new GameObjectBuilder(world, View, Content).CreateDefaultTank();

        }


        private void InitialzeWorld()
        {
            View = Matrix.Identity;

            Matrix matRotation = Matrix.CreateRotationZ(0);
            Matrix matZoom = Matrix.CreateScale(0.3f);

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
            tank.Update();

            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(0, null, null, null, null, null, View);
            tank.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
