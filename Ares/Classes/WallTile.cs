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
    public abstract class WallTile : Tile
    {
        public WallTile(Vector2f position, long UID_Builder)
			: base(position, UID_Builder)
        {
            Walkable = false;
        }
    }
}
