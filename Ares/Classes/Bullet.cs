using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using Lidgren.Network;

namespace Ares.Classes
{
    public class Bullet : Projectile
    {
        public Bullet(Vector2f pos, float angle, float speed)
            : base()
        {
            Position = pos;
            Angle = angle;
            Speed = speed;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Move()
        {
            if (Game.map.getTileInWorld(Position.X + Velocity.X, Position.Y + Velocity.Y).Walkable ||
                Game.map.getTileInWorld(Position.X + Velocity.X, Position.Y + Velocity.Y).PillBox)
                this.Position += Velocity;
            else
            {
                
            }
            base.Move();
        }
    }
}
