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
    public class Actor
    {
        public Vector2f Position { get; set; }
        public Vector2f Velocity { get; set; }
    }
}
