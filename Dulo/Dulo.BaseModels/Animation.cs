using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.BaseModels
{
    public class Animation : IUpdater
    {
        public List<Texture2D> Frames { get; set; }

        public int AnimationSpeed { get; set; } = 500;

        public Texture2D CurrentFrame
        {
            get
            {
                return Frames[currentFrameIndex];
            }
        } 


        private int currentFrameIndex = 0;

        private int beginTime { get; set; }

        private bool isPlaying = false;


        public Animation()
        {
            Frames = new List<Texture2D>();
        }

        public void Play()
        {
            beginTime = DateTime.Now.Millisecond;
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

        public void Update()
        {
            if (!isPlaying)
                return;

            if (NowMillisecond() < beginTime + AnimationSpeed)
                return;
            
            currentFrameIndex = currentFrameIndex < Frames.Count ? currentFrameIndex + 1 : 0;
        }


        private int NowMillisecond()
        {
            return DateTime.Now.Millisecond;
        }
    }
}
