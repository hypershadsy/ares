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
        public Vector2i Velocity;
        public int MovementSpeed, DefaultMovementSpeed;
        public string Name = "";
        public int Health, MaxHealth;

        public bool alive { get { return Health > 0; } }
    }
}
