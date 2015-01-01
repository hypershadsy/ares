using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace Ares
{
    class WallTile : Tile
    {
		public WallTile(Vector2f position)
			: base(position)
		{
		}

		public override void Draw()
		{
			base.Draw();
		}
    }
}
