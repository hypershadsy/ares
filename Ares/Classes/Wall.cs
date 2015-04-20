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
    public class Wall
    {

        public bool LeftFacing; //A left facing wall is rotated such that it sits among the left side of the tile
                                //A non left facing tile (top facing) sits among the top of the tile
        public Vector2i Position; //

        public Vector2i IsoCoords
        {
            get
            {
                return Helper.TileToIso(Position);
            }
        }

        public Wall(Vector2i position, bool leftFacing)
        {
            this.LeftFacing = leftFacing;
            Position = position;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(float drawLayer)
        {
        }

        protected void DefaultDraw(Texture texture, float drawLayer)
        {
            var tOrigin = new Vector2f(32f, 47f);
            var tRot = 0f;
            Color tCol = Color.White;
            if (IsoCoords.X / 32 % 2 == 0)
                tCol = new Color(190, 190, 190);
            int tFacing = LeftFacing ? 1 : -1;
            Render.Draw(texture, IsoCoords.ToF(), tCol, tOrigin, tFacing, tRot, drawLayer);
        }

    }
}
