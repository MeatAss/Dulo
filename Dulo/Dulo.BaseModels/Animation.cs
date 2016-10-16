using Dulo.BasisModels;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BaseModels
{
    public enum AnimationState
    {
        Play,
        Stop,
        Pause
    }

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

        public bool IsReverse { get; set; } = false;

        private int currentFrameIndex = 0;

        private long beginTime;

        private AnimationState currentState = AnimationState.Stop;

        public Animation()
        {
            Frames = new List<Texture2D>();
        }

        public void Play()
        {
            if (currentState == AnimationState.Play)
                return;
                    
            if (currentState == AnimationState.Stop)    
                beginTime = DateTime.Now.ToMilliseconds();

            currentState = AnimationState.Play;

            if (IsReverse && currentFrameIndex == 0)
                currentFrameIndex = Frames.Count - 1;

            if (!IsReverse && currentFrameIndex == Frames.Count - 1)
                currentFrameIndex = 0;
        }

        public void Pause()
        {
            currentState = AnimationState.Pause;
        }

        public void Stop()
        {
            currentState = AnimationState.Stop;
            currentFrameIndex = 0;
        }

        public override void Update()
        {
            if (currentState != AnimationState.Play)
                return;

            if (DateTime.Now.ToMilliseconds() < beginTime + AnimationSpeed)
                return;
            
            if (!IsReverse)
                currentFrameIndex = currentFrameIndex < Frames.Count-1 ? currentFrameIndex + 1 : 0;
            else
                currentFrameIndex = currentFrameIndex > 0 ? currentFrameIndex - 1 : Frames.Count - 1;

            beginTime = DateTime.Now.ToMilliseconds();

            if (!IsCyclicAnimation && ((!IsReverse && currentFrameIndex == 0) || (IsReverse && currentFrameIndex == Frames.Count - 1)))
                EndAnimation();
        }

        private void EndAnimation()
        {
            Stop();
            OnAnimationEnded?.Invoke();
        }
    }
}
