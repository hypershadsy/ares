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
        public Vector2f PositionInter;

        public NetPlayer(long uid)
        {
            this.UID = uid;
            Position = new Vector2i(100, 100);
            PositionInter = Position.ToF();
            Name = "Cactus Fantastico";
        }

        public override void Update()
        {
            Vector2f diff = PositionInter - Position.ToF();
            PositionInter = Position.ToF() + (diff / 2f);
        }

        public override void Draw()
        {
            Render.Draw(Game.charTexture, PositionInter, Color.Yellow, new Vector2f(0, 0), 1, 0);
            Render.DrawString(Game.font, Name, PositionInter - new Vector2f(15, 10), Color.White, .3f, true);
        }
    }
}
