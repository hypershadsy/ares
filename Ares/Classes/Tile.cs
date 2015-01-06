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
    public class Tile
    {
		public Vector2f Position;
        public int id;
        public long UID_BUILD;
        public bool Walkable;

        public Tile(Vector2f position, long UID_Builder)
        {
            this.UID_BUILD = UID_Builder;
			Position = position;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }
    }
}
