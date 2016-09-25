using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BaseModels
{
    public class Animation : BaseBasis
    {
        public event Action OnAnimationEnded;

        public List<Texture2D> Frames { get; set; }

        public int AnimationSpeed { get; set; } = 500;

        public Texture2D CurrentFrame
        {
            get
            {
                return Frames[currentFrameIndex];
            }
        }

        public bool IsCyclicAnimation { get; set; } = false;

        private int currentFrameIndex = 0;

        private long beginTime;

        private bool isPlaying = false;

        public Animation()
        {
            Frames = new List<Texture2D>();
        }

        public void Play()
        {
            beginTime = DateTime.Now.ToMilliseconds();
            isPlaying = true;
        }

        public void Pause()
        {
            isPlaying = false;
        }

        public void Stop()
        {
            isPlaying = false;
            currentFrameIndex = 0;
        }

        public override void Update()
        {
            if (!isPlaying)
                return;

            if (DateTime.Now.ToMilliseconds() < beginTime + AnimationSpeed)
                return;
            
            currentFrameIndex = currentFrameIndex < Frames.Count-1 ? currentFrameIndex + 1 : 0;

            beginTime = DateTime.Now.ToMilliseconds();

            if (!IsCyclicAnimation && currentFrameIndex == 0)
                EndAnimation();
        }

        private void EndAnimation()
        {
            Stop();
            OnAnimationEnded?.Invoke();
        }
    }
}
