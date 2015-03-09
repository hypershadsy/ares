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
    public class BrickWall : Wall
    {
        public BrickWall(Vector2i position)
            : base(position)
        {
        }

        public override void Draw()
        {
            base.Draw();


        }


    }
}
