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
    public class Door : Wall
    {
        public bool open = false;
        public bool locked = false;

        public Door(Vector2i position, bool leftFacing)
            : base(position, leftFacing)
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(float drawLayer)
        {
            base.Draw(drawLayer);
        }
    }
}
