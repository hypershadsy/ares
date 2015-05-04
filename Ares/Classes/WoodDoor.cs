﻿using System;
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
    public class WoodDoor : Door
    {
        public WoodDoor(Vector2i position, bool leftFacing)
            : base(position, leftFacing)
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(float drawLayer)
        {
            DefaultDraw(Game.woodDoor1, drawLayer);
            base.Draw(drawLayer);
        }
    }
}
