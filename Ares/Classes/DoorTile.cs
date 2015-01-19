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
    public class DoorTile : Tile
    {
        public DoorTile(Vector2f pos, long builder) :
            base(pos, builder)
        {
            Walkable = false;
        }

        public override void Draw()
        {
            DefaultDraw(Game.doorClosedTexture, true);
        }
    }
}
