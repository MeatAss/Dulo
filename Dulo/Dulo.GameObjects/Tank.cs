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
        private readonly Track leftTrack;
        private readonly Track rightTrack;

        private readonly IMovingKeyMap movingKeyMap;
        private readonly IShootingKeyMap shootingKeyMap;
        private KeyListener keyListener;

        public float TurretSpeedRotation { get; set; }


        public Tank(World world, TankBody tankBody, Turret turret, Track leftTrack, Track rightTrack,  KeyMap keyMap)
        {
            movingKeyMap = keyMap;
            shootingKeyMap = keyMap;

            this.tankBody = tankBody;
            this.turret = turret;
            this.leftTrack = leftTrack;
            this.rightTrack = rightTrack;
            
            CreateRevoluteJointTurret(world, turret, new Vector2(0, 9.7f));

            float positionTrack = 36.5f;
            CreateWeldJointTrack(world, leftTrack, new Vector2(-positionTrack, 0f));
            CreateWeldJointTrack(world, rightTrack, new Vector2(positionTrack, 0f));
            
            InitializeKeyListener();
        }

        private void CreateRevoluteJointTurret(World world, Turret turret, Vector2 position)
        {
            var jointBodyTurret = new RevoluteJoint(tankBody.Body, turret.Body, FarseerPhysics.ConvertUnits.ToSimUnits(position));

            jointBodyTurret.LocalAnchorA -= FarseerPhysics.ConvertUnits.ToSimUnits(position);
            world.AddJoint(jointBodyTurret);
            tankBody.Body.IgnoreCollisionWith(turret.Body);
        }

        private void CreateWeldJointTrack(World world, Track track, Vector2 position)
        {
            var jointBodyTrack = new WeldJoint(tankBody.Body, track.Body, FarseerPhysics.ConvertUnits.ToSimUnits(position), Vector2.Zero);

            world.AddJoint(jointBodyTrack);
            tankBody.Body.IgnoreCollisionWith(track.Body);
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

            leftTrack.Draw(canvas);
            rightTrack.Draw(canvas);
            tankBody.Draw(canvas);
            turret.Draw(canvas);
        }

        public override void Update()
        {
            base.Update();

            keyListener.Check(Keyboard.GetState());

            leftTrack.Update();
            rightTrack.Update();
            tankBody.Update();
            turret.Update();
        }
    }
}
