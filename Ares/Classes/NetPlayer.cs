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
    public class NetPlayer : Player
    {


        public NetPlayer(long uid)
        {
            this.UID = uid;
            Position = new Vector2f(100, 100);
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            Render.Draw(Game.charTexture, Position, Color.Red, new Vector2f(0, 0), 1);
           
        }
    }
}
