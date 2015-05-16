using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using Lidgren.Network;

namespace Ares
{
    public class Player : Actor
    {
        public long UID;
        protected int frame = 0;
        protected float frameDelta = 0;

        public Player()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(float layer)
        {
        }

        protected void IncrementAnimationFrame()
        {
            frameDelta += (float)Game.deltaTime.TotalMilliseconds;
            if (frameDelta > 150f)
            {
                frameDelta = 0;
                frame++;
                frame %= 3; //total frames
            }
        }
    }
}
