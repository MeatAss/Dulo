using System.Collections.Generic;
using Dulo.GameObjects;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;
using Dulo.GameObjects.Guns;
using Dulo.GameObjects.SettingsModels;
using Dulo.InputModel.InputSystem;
using Dulo.InputModel.InputSystem.ConcreteInputSystems;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dulo
{
    public class GameObjectBuilder
    {
        private readonly Matrix matrixView;
        private readonly World world;
        private readonly ContentManager contentManager;

        public GameObjectBuilder(World world, Matrix matrixView, ContentManager contentManager)
        {
            this.contentManager = contentManager;
            this.world = world;
            this.matrixView = matrixView;
        }

        public Tank CreateDefaultTank()
        {
            //var keyMap = GetKeyboardInputSystemMap();
//            var keyMap = GetKeyboardInputWithMouseSystemMap();

            var tankBody = CreateTankBody();
            var turret = CreateTurret();
            var leftTrack = CreateTrack();
            var rightTrack = CreateTrack();

            var tank = new Tank(world, tankBody, turret, leftTrack, rightTrack);

            return tank;
        }        

        private Track CreateTrack()
        {
            var trackAnimation = CreateTrackDefaultAnimation();
            var settingTankBody = new SettingBaseAnimationModel(world, trackAnimation.CurrentFrame, trackAnimation);

            return new Track(settingTankBody);
        }        

        private Turret CreateTurret()
        {
            var turretAnimation = CreateTurretDefaultAnimation();
            var settingTurret = new SettingBaseAnimationModel(world, turretAnimation.CurrentFrame, turretAnimation);

            return new Turret(settingTurret, new MouseProcessor(matrixView), CreateDefaultGun());
        }

        private BaseGun CreateDefaultGun()
        {
            var bulletDefaultAnimation = CreateBulletDefaultAnimation();

            var settingBullet = new SettingBullet(world, bulletDefaultAnimation.CurrentFrame, 0.5f, bulletDefaultAnimation, 
                null, null, null);

            return new DefaultGun(settingBullet);
        }

        private TankBody CreateTankBody()
        {
            var tankBodyAnimation = CreateTankBodyDefaultAnimation();
            var settingTankBody = new SettingBaseAnimationModel(world, tankBodyAnimation.CurrentFrame, tankBodyAnimation);

            var tankBody = new TankBody(settingTankBody);

            tankBody.SpeedMoving = 300f;
            tankBody.SpeedRotation = 7f;
            tankBody.LinearDamping = 5f;
            tankBody.AngularDamping = 18f;

            return tankBody;
        }

        private Animation CreateTankBodyDefaultAnimation()
        {
            var  animation = new Animation();

            //for (var i = 0; i < 14; i++)
            //    animation.Frames.Add(contentManager.Load<Texture2D>($"tank/Tank{i}"));

            animation.Frames.Add(contentManager.Load<Texture2D>($"tankBody/TankBody"));

            return animation;
        }

        private Animation CreateTrackDefaultAnimation()
        {
            var animation = new Animation();

            for (var i = 0; i < 16; i++)
                animation.Frames.Add(contentManager.Load<Texture2D>($"track/Track{i}"));

            animation.IsCyclicAnimation = true;
            animation.AnimationSpeed = 100;
            return animation;
        }

        private Animation CreateTurretDefaultAnimation()
        {
            var animation = new Animation();

            for (int i = 1; i < 13; i++)
                animation.Frames.Add(contentManager.Load<Texture2D>($"gun1/{i}"));

            animation.AnimationSpeed = 1;
            animation.IsCyclicAnimation = false;
            return animation;
        }

        private Animation CreateBulletDefaultAnimation()
        {
            var animation = new Animation();

            animation.Frames.Add(contentManager.Load<Texture2D>("bullets/bullet"));

            return animation;
        }
    }
}
