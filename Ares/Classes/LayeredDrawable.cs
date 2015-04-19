using System;
using SFML.Graphics;

namespace Ares
{
    public class LayeredDrawable
    {
        public float Layer { get; set; }
        public Drawable Drawable { get; set; }
    }
}
