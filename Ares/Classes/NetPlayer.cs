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
		public Vector2f PositionGoal;

        public NetPlayer(long uid)
        {
            this.UID = uid;
            Position = new Vector2f(100, 100);
			PositionGoal = Position;
        }

        public override void Update()
        {
			Vector2f diff = PositionGoal - Position;
			Position = Position + diff / 2f;
        }

        public override void Draw()
        {
            Render.Draw(Game.charTexture, Position, Color.Yellow, new Vector2f(0, 0), 1, 0);
           
        }
    }
}
