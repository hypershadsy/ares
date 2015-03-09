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
    public class RedBrickWall : Wall
    {
        public RedBrickWall(Vector2i position, bool leftFacing)
            : base(position, leftFacing)
        {
        }

        public override void Draw()
        {
            DefaultDraw(Game.brickWallTexture);
        }
    }
}
