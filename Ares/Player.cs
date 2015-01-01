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

        public Player()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
            Render.Draw(Game.charSprite, Position, Color.White, new Vector2f(0, 0), 1);
        }
    }
}
