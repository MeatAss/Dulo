using Dulo.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.Dulo.Action
{
    public class Shooter
    {
        public Models.Model Model { get; set; }

        private List<Bullet> bullets = new List<Bullet>();

        private Texture2D textureBullet;

        private Vector2 size;

        public Shooter(Models.Model model, Texture2D textureBullet, Vector2 size)
        {
            Model = model;
            this.textureBullet = textureBullet;
            this.size = size;
        }

        public void Fire()
        {
            var bullet = new Bullet(textureBullet, Model.Position, size);
            bullet.Angle = Model.Angle;
            bullets.Add(bullet);
        }

        public void Update()
        {
            bullets.ForEach(b => b.Mover.MoveTo(15));   
        }

        public void Render(SpriteBatch spriteBatch)
        {
            bullets.ForEach(b => ((IModel)b).Render(spriteBatch));       
        }
    }
}
