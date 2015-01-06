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
    public abstract class GameObject
    {
        public Vector2f Position;
        public Vector2f Velocity;
        public float Speed;
        public float Angle, Rotation;
        public long OwnerUID;


        public GameObject()
        {
        }

        public GameObject(Vector2f position)
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }

        public virtual void Activate()
        {
        }
    }
}
