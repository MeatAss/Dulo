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
        public Turret Turret;
        public TankBody TankBody;
        public Track LeftTrack;
        public Track RightTrack;
        public TankCollider Collider;

        private ManagerGameOperationAction managerKeyDown;
        private ManagerGameOperationAction managerKeyUp;


        public Vector2 Position
        {
            get { return TankBody.Position; }

            set { TankBody.Position = value; }
        }

        public Tank(World world, TankCollider tankCollider, TankBody tankBody, Turret turret, Track leftTrack, Track rightTrack)
        {
            InitializeVaribles(tankCollider, tankBody, turret, leftTrack, rightTrack);
            InitializeInputSystem();

            BuildTank(world);

            InitializeManagerKeyDown();
            InitializeManagerKeyUp();
        }

        public void SetDefaultSettings()
        {
            Collider.Body.Mass = 100f;

            Turret.Body.Mass = 10f;
            Turret.SpeedRotation = 5f;
            Turret.Body.AngularDamping = 50f;

            TankBody.SpeedMoving = 600f;
            TankBody.SpeedRotation = 7f;
            TankBody.LinearDamping = 30f;
            TankBody.AngularDamping = 80f;
        }

        public override void Draw(SpriteBatch canvas)
        {
            base.Draw(canvas);
            LeftTrack.Draw(canvas);
            RightTrack.Draw(canvas);
            TankBody.Draw(canvas);
            Turret.Draw(canvas);
        }

        public override void Update()
        {
            base.Update();

            LeftTrack.Update();
            RightTrack.Update();
            TankBody.Update();
            Turret.Update();
        }

        private void InitializeVaribles(TankCollider tankCollider, TankBody tankBody, Turret turret, Track leftTrack, Track rightTrack)
        {
            Collider = tankCollider;
            TankBody = tankBody;
            Turret = turret;
            LeftTrack = leftTrack;
            RightTrack = rightTrack;
        }

        private void InitializeInputSystem()
        {
            var input = KeyListener.Sender;
            input.OnKeyDown += Input_OnKeyDown;
            input.OnKeyUp += Input_OnKeyUp;
        }

        private void BuildTank(World world)
        {
            CreateRevoluteJointTurret(world, Turret, new Vector2(0, 9.7f));

            float positionTrack = 36.5f;

            DisableCollisionForVisibleObject();

            CreateWeldJoint(world, Collider.Body, LeftTrack.Body, new Vector2(-positionTrack, 0f), Vector2.Zero);
            CreateWeldJoint(world, Collider.Body, RightTrack.Body, new Vector2(positionTrack, 0f), Vector2.Zero);

            CreateWeldJoint(world, Collider.Body, TankBody.Body, Vector2.Zero, Vector2.Zero);
        }

        private void DisableCollisionForVisibleObject()
        {
            LeftTrack.Body.CollisionCategories = Category.None;
            RightTrack.Body.CollisionCategories = Category.None;
            TankBody.Body.CollisionCategories = Category.None;

            Turret.Body.IgnoreCollisionWith(LeftTrack.Body);
            Turret.Body.IgnoreCollisionWith(RightTrack.Body);
            Turret.Body.IgnoreCollisionWith(TankBody.Body);
            Turret.Body.IgnoreCollisionWith(Collider.Body);
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
            var jointBodyTurret = new RevoluteJoint(Collider.Body, turret.Body, FarseerPhysics.ConvertUnits.ToSimUnits(position));

            jointBodyTurret.LocalAnchorA -= FarseerPhysics.ConvertUnits.ToSimUnits(position);
            world.AddJoint(jointBodyTurret);
        }

        private void CreateWeldJoint(World world, Body bodyA, Body bodyB, Vector2 positionA, Vector2 positionB)
        {
            var jointBodyTrack = new WeldJoint(bodyA, bodyB, FarseerPhysics.ConvertUnits.ToSimUnits(positionA), FarseerPhysics.ConvertUnits.ToSimUnits(positionB));

            world.AddJoint(jointBodyTrack);
        }

        private void InitializeManagerKeyDown()
        {
            managerKeyDown = new ManagerGameOperationAction();

            managerKeyDown.Add(GameOperation.MoveUp, MoveUp);
            managerKeyDown.Add(GameOperation.MoveDown, MoveDown);
            managerKeyDown.Add(GameOperation.TurnLeft, TurnLeft);
            managerKeyDown.Add(GameOperation.TurnRight, TurnRight);

            managerKeyDown.Add(GameOperation.RotateTurretLeft, () => Turret.Rotate(-Turret.SpeedRotation));
            managerKeyDown.Add(GameOperation.RotateTurretRight, () => Turret.Rotate(Turret.SpeedRotation));

            managerKeyDown.Add(GameOperation.Fire, Fire);
        }

        private void InitializeManagerKeyUp()
        {
            managerKeyUp = new ManagerGameOperationAction();

            Action eventUpDown = () => {
                LeftTrack.AnimationPause();
                RightTrack.AnimationPause();
            };

            managerKeyUp.Add(GameOperation.MoveUp, eventUpDown);
            managerKeyUp.Add(GameOperation.MoveDown, eventUpDown);
            managerKeyUp.Add(GameOperation.TurnLeft, eventUpDown);
            managerKeyUp.Add(GameOperation.TurnRight, eventUpDown);
        }

        private void MoveDown()
        {
            Collider.MoveTo(-TankBody.SpeedMoving * 0.6f);

            PlayTrakAnimation(LeftTrack, true);
            PlayTrakAnimation(RightTrack, true);
        }

        private void MoveUp()
        {
            Collider.MoveTo(TankBody.SpeedMoving);

            PlayTrakAnimation(LeftTrack, false);
            PlayTrakAnimation(RightTrack, false);
        }

        private void TurnRight()
        {
            Collider.Rotate(TankBody.SpeedRotation);

            RightTrack.AnimationPause();
            PlayTrakAnimation(LeftTrack, false);
        }

        private void TurnLeft()
        {
            Collider.Rotate(-TankBody.SpeedRotation);

            LeftTrack.AnimationPause();
            PlayTrakAnimation(RightTrack, false);

            Turret.SpeedRotation = 5 + TankBody.SpeedRotation;
        }

        public void PlayTrakAnimation(Track track, bool isRevers)
        {
            track.IsReverse = isRevers;
            track.AnimationPlay();
        }

        private void Fire()
        {
            var bullet = Turret.Fire();

            if (bullet == null)
                return;

            Turret.AnimationPlay();

            SetIgnorPhysicsTime(250, bullet.Body, Collider.Body);
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
