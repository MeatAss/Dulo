using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Dulo
{

    public class FrameCounter : DrawableGameComponent
    {
        private TimeSpan _elapsedTime = TimeSpan.Zero;
        private NumberFormatInfo _format;
        private int _frameCounter;
        private int _frameRate;
        private Vector2 _position;
        private SpriteBatch spriteBatch;
        private SpriteFont font;

        public FrameCounter(Game game, SpriteBatch spriteBatch, SpriteFont font)
            : base(game)
        {
            _format = new NumberFormatInfo();
            _format.NumberDecimalSeparator = ".";

            _position = new Vector2(30, 25);
            this.spriteBatch = spriteBatch;
            this.font = font;
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime <= TimeSpan.FromSeconds(1)) return;

            _elapsedTime -= TimeSpan.FromSeconds(1);
            _frameRate = _frameCounter;
            _frameCounter = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            _frameCounter++;

            string fps = string.Format(_format, "{0} fps", _frameRate);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, fps, _position + Vector2.One, Color.Black);
            spriteBatch.DrawString(font, fps, _position, Color.YellowGreen);
            spriteBatch.End();
        }
    }
}
