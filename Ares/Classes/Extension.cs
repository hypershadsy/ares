using System;
using SFML.Window;

namespace Ares
{
    public static class Extension
    {
        public static Vector2f ToF(this Vector2i input)
        {
            return new Vector2f(input.X, input.Y);
        }
    }
}

