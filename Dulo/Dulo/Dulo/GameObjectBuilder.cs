using Dulo.GameObjects;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;
using Dulo.GameObjects.Guns;
using Dulo.GameObjects.SettingsModels;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            //var keyMap = GetKeyboardInputWithMouseSystemMap();

            var tankBody = CreateTankBody();
            var turret = CreateTurret();
            var leftTrack = CreateTrack();
            var rightTrack = CreateTrack();
            var tankcollidet = CreateTankCollider();

            var tank = new Tank(world, tankcollidet, tankBody, turret, leftTrack, rightTrack);

            tank.SetDefaultSettings();

            return tank;
        }

        private TankCollider CreateTankCollider()
        {
            var settingTank = new SettingBaseModel(world, contentManager.Load<Texture2D>($"tank/Tank1"));

            return new TankCollider(settingTank);
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

            return tankBody;
        }

        public Wall CreateWall()
        {
            var animation = CreateWallDefaultAnimation();

            var wall = new Wall(new SettingWall(world, animation.CurrentFrame, animation));



            return wall;
        }

        private Animation CreateTankBodyDefaultAnimation()
        {
            var  animation = new Animation();

            //for (var i = 0; i < 14; i++)
            //    animation.Frames.Add(contentManager.Load<Texture2D>($"tank/Tank{i}"));

            animation.Frames.Add(contentManager.Load<Texture2D>($"TankBody/TankBody"));

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

        private Animation CreateWallDefaultAnimation()
        {
            var animation = new Animation();

            animation.Frames.Add(contentManager.Load<Texture2D>("walls/wall"));

            return animation;
        }
    }
}
