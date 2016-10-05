using Dulo.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dulo.InputModel;
using Dulo.Actions;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;

namespace Dulo.GameObjects
{
    public class Tank : BaseAnimationModel 
    {
        public Turret turret;

        private IMovingKeyMap movingKeyMap;
        private IShootingKeyMap shootingKeyMap;
        private KeyListener keyListener;

        private Mover mover;

        private float speedMoving;
        private float speedRotating;

        public float TurretSpeedRotation { get; set; }

        public float SpeedMoving
        {
            get
            {
                return speedMoving;
            }
            set
            {
                speedMoving = value < 0 ? 0 : value;
            }
        }

        public float SpeedRotating
        {
            get
            {
                return speedRotating;
            }
            set
            {
                speedRotating = value < 0 ? 0 : value;
            }
        }


        public Tank(World world, Texture2D physicalTextureMapTank, Texture2D physicalTextureMapTurret, Texture2D physicalTextureMapBullet, KeyMap keyMap) : base(world, physicalTextureMapTank)
        {
            movingKeyMap = keyMap;
            shootingKeyMap = keyMap;

            mover = new Mover(this, 5f, 18f);
            InitializeKeyListener();

            turret = new Turret(world, physicalTextureMapTurret, physicalTextureMapBullet);
            turret.Body.Mass = 0.1f;

            turret.Body.AngularDamping = 15f;

            var a = new RevoluteJoint(Body, turret.Body, FarseerPhysics.ConvertUnits.ToSimUnits(new Vector2(0, 9.7f)));

            a.LocalAnchorA -= FarseerPhysics.ConvertUnits.ToSimUnits(new Vector2(0, 9.7f));

            world.AddJoint(a);


            Body.IgnoreCollisionWith(turret.Body);
        }

        private void InitializeKeyListener()
        {
            keyListener = new KeyListener();
            keyListener.Add(movingKeyMap.Up, () => mover.MoveTo(speedMoving));
            keyListener.Add(movingKeyMap.Down, () => mover.MoveTo(-speedMoving * 0.6f));
            keyListener.Add(movingKeyMap.Left, () => mover.Rotate(-speedRotating));
            keyListener.Add(movingKeyMap.Right, () => mover.Rotate(speedRotating));
            keyListener.Add(shootingKeyMap.Fire, () => turret.Fire());
        }

        public override void Draw(SpriteBatch canvas)
        {        
            base.Draw(canvas);
            turret.Draw(canvas);
        }

        public override void Update()
        {
            base.Update();

            turret.Update();

            keyListener.Check(Keyboard.GetState());    
        }       
        
        public void AddTurretAnimation(Animation animation, string animationName)
        {
            turret.AddNewAnimation(animation, animationName);
        } 

        public void ChangeTurretAnimation(string animationName)
        {
            turret.ChangeAnimation(animationName);
        }

        public void TurretAnimationPlay()
        {
            turret.AnimationPlay();
        }
    }
}
