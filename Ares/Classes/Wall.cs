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

        public virtual void Draw()
        {
        }

        protected void DefaultDraw(Texture texture)
        {
            //iso: bottom, then top
            var wOrigin = new Vector2f(32, 0);
            var wFacing = 1;
            var wRot = 0f;
            Color wCol = Color.White;//= Walkable ? Color.White : Color.Red;
            if (IsoCoords.X / 32 % 2 == 0)
                wCol = new Color(190, 190, 190);
            Render.Draw(Game.woodfloor, IsoCoords.ToF(), wCol, wOrigin, wFacing, wRot);
        }

    }
}
