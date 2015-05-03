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

        public Vector2i IsoCoords
        {
            get
            {
                return Helper.TileToIso(Position);
            }
        }

        public Tile(Vector2i position)
        {
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
            var tOrigin = new Vector2f(32, 0);
            var tFacing = 1;
            var tRot = 0f;
            Color tCol;
            if (IsoCoords.X / 32 % 2 == 0)
                tCol = new Color(190, 190, 190);
            Render.Draw(texture, IsoCoords.ToF(), tCol, tOrigin, tFacing, tRot, Layer.Floor);
        }
    }
}
