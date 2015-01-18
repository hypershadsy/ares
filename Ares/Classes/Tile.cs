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
        public bool PillBox;
		public Vector2f IsoCoords
		{
			get
			{
				return new Vector2f(Position.X * 30 - (Position.Y * 30), Position.Y * 17 + (Position.X * 17) - 13);
			}
		}

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

		protected void DefaultDraw(Texture texture, bool drawTopBlock)
		{
			//iso: bottom, then top
			var tOrigin = new Vector2f(0, 0);
			var tFacing = 1;
			var tRot = 0f;
			Render.Draw(Game.isoBlock, IsoCoords, Color.White, tOrigin, tFacing, tRot);
			if (drawTopBlock)
			{
				var tTopPos = new Vector2f(IsoCoords.X, IsoCoords.Y - 13);
				Render.Draw(Game.isoBlock, tTopPos, Color.Magenta, tOrigin, tFacing, tRot);
			}

			//topdown
			Render.Draw(texture, Position * 32, Color.White, new Vector2f(0, 0), 1, 0);
		}
    }
}
