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
    public class Actor
    {
        public Vector2i Position;
        public string Name = "";
        public int Health, MaxHealth;
        public Vector2i IsoPosition
        {
            get
            {
                Vector2i ret = Helper.TileToIso(Position);
                ret.X += 32;
                ret.Y += 16;
                return ret; //isospace pos of feet standing at center of tile
            }
        }

        public bool alive { get { return Health > 0; } }
    }
}
