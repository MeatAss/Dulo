using System.Collections.Generic;
using System.Linq;
using Dulo.BaseModels.SettingsModels;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace Dulo.BaseModels
{
    public abstract class BaseAnimationModel : BaseModel
    {
        private readonly Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        private string currentAnimationName = "";

        protected BaseAnimationModel(World world, Texture2D physicalTextureMap, Animation defauleAnimation) : base(world, physicalTextureMap)
        {
            AddNewAnimation(defauleAnimation, "default");
            ChangeAnimation("default");
        }

        protected BaseAnimationModel(SettingBaseAnimationModel settingBaseAnimationModel) : base(settingBaseAnimationModel)
        {
            AddNewAnimation(settingBaseAnimationModel.DefaultAnimation, "default");
            ChangeAnimation("default");
        }

        public override void Update()
        {
            if (animations.Count == 0 || string.IsNullOrEmpty(currentAnimationName))
                return;

            animations[currentAnimationName].Update();
            Texture = animations[currentAnimationName].CurrentFrame;
        }

        public void AddNewAnimation(Animation animation, string animationName)
        {
            animations.Add(animationName, animation);
        }

        public void RemoveAnimation(string animationName)
        {
            animations.Remove(animationName);
        }

        public bool ChangeAnimation(string animationName)
        {
            if (!IsExistAnimation(animationName))
                return false;

            if (animationName != "")
                animations[animationName].Stop();

            currentAnimationName = animationName;
            Texture = animations[animationName].CurrentFrame;
            return true;
        }

        private bool IsExistAnimation(string animationName)
        {
            animationName = animations.Keys.FirstOrDefault((item) => item == animationName);
            return animationName != null;
        }

        public void AnimationPlay()
        {
            animations[currentAnimationName].Play();
        }

        public void AnimationPause()
        {
            if (string.IsNullOrEmpty(currentAnimationName))
                return;

            animations[currentAnimationName].Pause();
        }

        public void AnimationStop()
        {
            if (string.IsNullOrEmpty(currentAnimationName))
                return;

            animations[currentAnimationName].Stop();
        }
    }
}
