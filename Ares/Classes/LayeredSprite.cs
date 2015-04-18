using System;
using SFML.Graphics;

namespace Ares
{
    public class LayeredSprite : Sprite
    {
        public float Layer { get; set; }

        public LayeredSprite(Texture t)
            : base (t)
        {
        }
    }
}

