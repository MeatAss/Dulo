using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BaseModels
{
    public abstract class BaseAnimationModel : BaseModel
    {
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        private string animationName = "";

        public override void Update()
        {
            if (animations.Count == 0 || string.IsNullOrEmpty(animationName))
                return;

            animations[animationName].Update();
            texture = animations[animationName].CurrentFrame;
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

            this.animationName = animationName;
            animations[animationName].Stop();
            return true;
        }

        private bool IsExistAnimation(string animationName)
        {
            animationName = animations.Keys.FirstOrDefault((item) => item == animationName);
            return animationName != null;
        }

        public void AnimationPlay()
        {
            if (string.IsNullOrEmpty(animationName))
                return;

            animations[animationName].Play();
        }

        public void AnimationPause()
        {
            if (string.IsNullOrEmpty(animationName))
                return;

            animations[animationName].Pause();
        }

        public void AnimationStop()
        {
            if (string.IsNullOrEmpty(animationName))
                return;

            animations[animationName].Stop();
        }
    }
}
