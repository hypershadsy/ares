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
    public static class Helper
    {

        public static float Distance(Vector2f vec1, Vector2f vec2)
        {
            return (float)Math.Sqrt(Math.Pow((vec2.X - vec1.X), 2) + Math.Pow((vec2.Y - vec1.Y), 2));
        }
    }
}
