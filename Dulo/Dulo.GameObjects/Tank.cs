using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dulo.InputModel;
using Dulo.BasisModels;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;

namespace Dulo.GameObjects
{
    public class Tank : BaseBasis
    {
        private readonly Turret turret;
        private readonly TankBody tankBody;

        private readonly IMovingKeyMap movingKeyMap;
        private readonly IShootingKeyMap shootingKeyMap;
        private KeyListener keyListener;

        public float TurretSpeedRotation { get; set; }


        public Tank(World world, TankBody tankBody, Turret turret, KeyMap keyMap)
        {
            movingKeyMap = keyMap;
            shootingKeyMap = keyMap;

            this.tankBody = tankBody;
            this.turret = turret;

            var revoluteJoint = new RevoluteJoint(tankBody.Body, turret.Body, FarseerPhysics.ConvertUnits.ToSimUnits(new Vector2(0, 9.7f)));

            revoluteJoint.LocalAnchorA -= FarseerPhysics.ConvertUnits.ToSimUnits(new Vector2(0, 9.7f));
            world.AddJoint(revoluteJoint);
            tankBody.Body.IgnoreCollisionWith(turret.Body);

            InitializeKeyListener();
        }

        private void InitializeKeyListener()
        {
            keyListener = new KeyListener();
            keyListener.Add(movingKeyMap.Up, () => tankBody.MoveTo(tankBody.SpeedMoving));
            keyListener.Add(movingKeyMap.Down, () => tankBody.MoveTo(-tankBody.SpeedMoving * 0.6f));
            keyListener.Add(movingKeyMap.Left, () => tankBody.Rotate(-tankBody.SpeedRotating));
            keyListener.Add(movingKeyMap.Right, () => tankBody.Rotate(tankBody.SpeedRotating));

            keyListener.Add(shootingKeyMap.Fire, turret.Fire);
        }

        public override void Draw(SpriteBatch canvas)
        {        
            base.Draw(canvas);
           
            tankBody.Draw(canvas);
            turret.Draw(canvas);
        }

        public override void Update()
        {
            base.Update();

            keyListener.Check(Keyboard.GetState());

            tankBody.Update();
            turret.Update();
        }
    }
}
