using System;
using SFML.Graphics;

namespace Ares
{
    public class LayeredDrawable
    {
        public float Layer { get; set; }
        public Drawable Drawable { get; set; }
    }

    public static class Layer
    {
        public const float LowerWalls = 0.75f;
        public const float Floor = 0.7f;
        public const float WallFar = 0.6f;
        public const float WallNear = 0.2f;
    }
}
