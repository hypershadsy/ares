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
    public class Door : Tile
    {
        public Door(Vector2f pos, long builder) :
            base(pos, builder)
        {
            Walkable = false;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }

}
