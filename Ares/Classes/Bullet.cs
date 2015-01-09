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
    public class Bullet : Projectile
    {
        public Bullet(Vector2f pos, float angle, float speed, long UID)
            : base()
        {
            Position = pos;
            Angle = angle;
            Speed = speed;
        }

        public override void Update()
        {
            Move();
            Velocity = new Vector2f(
                (float)Math.Cos((double)Angle),
                (float)Math.Sin((double)Angle)) * Speed;

            base.Update();
        }

        public override void Draw()
        {

            //Console.WriteLine(Helper.DegToRad(Angle));

            Render.Draw(Game.bulletTexture, Position, Color.White,
            new Vector2f(Game.bulletTexture.Size.X, Game.bulletTexture.Size.Y) / 2, 1, Helper.RadToDeg(Angle));

            base.Draw();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Move()
        {
            try
            {
                if (Game.map.getTileInWorld(Position.X + 32 + Velocity.X, Position.Y + Velocity.Y).Walkable)
                    this.Position += Velocity;
                else
                {
                    Destroy();
                }
            }
            catch (Exception)
            {
                Destroy();
            }
            base.Move();
        }
    }
}
