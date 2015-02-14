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
        public Vector2i Position;
        public int id;
        public long UID_BUILD;
        public bool Walkable;
        public bool PillBox;

        public Vector2f IsoCoords
        {
            get
            {
                int realX = 0;
                int realY = 0;
                realX += Position.X * 32;
                realX -= Position.Y * 32;
                realY += Position.Y * 16;
                realY += Position.X * 16;
                return new Vector2f(realX, realY);
            }
        }

        public Tile(Vector2i position, long UID_Builder)
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

        protected void DefaultDraw(Texture texture)
        {
            //iso: bottom, then top
            var tOrigin = new Vector2f(0, 0);
            var tFacing = 1;
            var tRot = 0f;
            Color tCol = Walkable ? Color.White : Color.Red;
            Render.Draw(Game.tileBedug, IsoCoords, tCol, tOrigin, tFacing, tRot);
        }
    }
}
