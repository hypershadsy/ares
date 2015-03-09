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
    public class RedBrick : Wall
    {
        public RedBrick(Vector2i position, bool leftFacing)
            : base(position, leftFacing)
        {
        }

        public override void Draw()
        {
            base.Draw();

            if (LeftFacing)
            {
                var tOrigin = new Vector2f(32, 0);
                var tFacing = 1;
                var tRot = 0f;
                Color tCol = Color.White;
                if (IsoCoords.X / 32 % 2 == 0)
                    tCol = new Color(190, 190, 190);
                Render.Draw(Game.brickWallTexture, IsoCoords.ToF(), tCol, tOrigin, tFacing, tRot);
            }
            if (!LeftFacing) //I'm not using 'else' because this is more explicit
            {
                var tOrigin = new Vector2f(32, 0);
                var tFacing = -1; //Set the sprite to mirror if it's not left facing
                var tRot = 0f;
                Color tCol = Color.White;
                if (IsoCoords.X / 32 % 2 == 0)
                    tCol = new Color(190, 190, 190);
                Render.Draw(Game.brickWallTexture, IsoCoords.ToF(), tCol, tOrigin, tFacing, tRot);
            }
        }


    }
}
