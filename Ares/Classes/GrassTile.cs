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
    public class GrassTile : GroundTile
    {
        public GrassTile(Vector2f position, long UID_Builder)
            : base(position, UID_Builder)
        {
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            Render.Draw(Game.isoBlock, new Vector2f(Position.X * 30 - (Position.Y * 30), Position.Y * 17 + (Position.X * 17) - 13), Color.White, new Vector2f(0, 0), 1, 0f);
               
            //Vector2f drawPos = new Vector2f(Position.X * 30, Position.Y * 18);
            Render.Draw(Game.grassTexture, Position * 32, Color.White, new Vector2f(0, 0), 1, 0);
        }
    }
}
