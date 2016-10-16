using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Dulo.InputModel;
using Dulo.BasisModels;
using Dulo.InputModel.InputSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using System;

namespace Dulo.GameObjects
{
    public class Tank : BaseBasis
    {
        private readonly Turret turret;
        private readonly TankBody tankBody;
        private readonly Track leftTrack;
        private readonly Track rightTrack;

        private ManagerGameOperationAction managerKeyDown;
        private ManagerGameOperationAction managerKeyUp;

        private readonly KeyListener input;

        public Vector2 Position
        {
            get { return tankBody.Position; }

            set { tankBody.Position = value; }
        }

        public Tank(World world, TankBody tankBody, Turret turret, Track leftTrack, Track rightTrack)
        {
            this.tankBody = tankBody;
            this.turret = turret;
            this.leftTrack = leftTrack;
            this.rightTrack = rightTrack;

            input = KeyListener.Sender;
            input.OnKeyDown += Input_OnKeyDown;
            input.OnKeyUp += Input_OnKeyUp;

            CreateRevoluteJointTurret(world, turret, new Vector2(0, 9.7f));

            float positionTrack = 36.5f;
            CreateWeldJointTrack(world, leftTrack, new Vector2(-positionTrack, 0f));
            CreateWeldJointTrack(world, rightTrack, new Vector2(positionTrack, 0f));

            InitializeManagerKeyDown();
            InitializeManagerKeyUp();
        }

        private void Input_OnKeyUp(GameOperation gameOperation)
        {
            managerKeyUp.Check(gameOperation);
        }

        private void Input_OnKeyDown(GameOperation gameOperation)
        {
            managerKeyDown.Check(gameOperation);
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
        }

        private void InitializeManagerKeyDown()
        {
            managerKeyDown = new ManagerGameOperationAction();

            managerKeyDown.Add(GameOperation.MoveUp, () => MoveUp());
            managerKeyDown.Add(GameOperation.MoveDown, () => MoveDown());
            managerKeyDown.Add(GameOperation.TurnLeft, () => tankBody.Rotate(-tankBody.SpeedRotation));
            managerKeyDown.Add(GameOperation.TurnRight, () => tankBody.Rotate(tankBody.SpeedRotation));

            managerKeyDown.Add(GameOperation.RotateTurretLeft, () => turret.Rotate(-turret.SpeedRotation));
            managerKeyDown.Add(GameOperation.RotateTurretRight, () => turret.Rotate(turret.SpeedRotation));

            managerKeyDown.Add(GameOperation.Fire, Fire);
        }

        private void InitializeManagerKeyUp()
        {
            managerKeyUp = new ManagerGameOperationAction();

            Action eventUpDown = () => {
                leftTrack.AnimationPause();
                rightTrack.AnimationPause();
            };

            managerKeyUp.Add(GameOperation.MoveUp, eventUpDown);
            managerKeyUp.Add(GameOperation.MoveDown, eventUpDown);
        }

        private void MoveDown()
        {
            tankBody.MoveTo(-tankBody.SpeedMoving * 0.6f);

            leftTrack.IsReverse = true;
            rightTrack.IsReverse = true;
            leftTrack.AnimationPlay();
            rightTrack.AnimationPlay();            
        }

        private void MoveUp()
        {
            tankBody.MoveTo(tankBody.SpeedMoving);

            leftTrack.IsReverse = false;
            rightTrack.IsReverse = false;
            leftTrack.AnimationPlay();
            rightTrack.AnimationPlay();
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
            
            leftTrack.Update();
            rightTrack.Update();
            tankBody.Update();
            turret.Update();
        }

        private void Fire()
        {
            var bullet = turret.Fire();

            if (bullet == null)
                return;

            turret.AnimationPlay();

            SetIgnorPhysicsTime(250, bullet.Body, leftTrack.Body, rightTrack.Body);
        }

        private void SetIgnorPhysicsTime(int lifetime, Body body, params Body[] targets)
        {
            foreach (var target in targets)
                body.IgnoreCollisionWith(target);

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(lifetime);

                foreach (var target in targets)
                    body.RestoreCollisionWith(target);
            });
        }

    }
}
