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
    public class WoodWallTile : WallTile
    {
        public WoodWallTile(Vector2f position, long UID_Builder)
            : base(position, UID_Builder)
        {
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            Render.Draw(Game.wallTexture, Position * 32, Color.White, new Vector2f(0, 0), 1);
        }
    }
}
