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

        public Tile(Vector2f position)
        {
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
