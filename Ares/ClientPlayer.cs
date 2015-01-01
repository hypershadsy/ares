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
    public class ClientPlayer : Player
    {
        public ClientPlayer()
            : base()
        {
            Position = new Vector2f(100, 100);
        }

        public override void Update()
        {
            HandleMovement();
            base.Update();
        }

        public override void Draw()
        {
            Render.Draw(Game.charTexture, Position, Color.White, new Vector2f(0, 0), 1);
            base.Draw();
        }

        void HandleMovement()
        {
            if (Input.isKeyDown(Keyboard.Key.A))
            {
                Velocity.X = -speed;
            }
            if (Input.isKeyDown(Keyboard.Key.D))
            {
                Velocity.X = speed;
            }

            if (Input.isKeyDown(Keyboard.Key.W))
            {
                Velocity.Y = -speed;
            }
            if (Input.isKeyDown(Keyboard.Key.S))
            {
                Velocity.Y = speed;
            }

            Position += Velocity;
        }
    }
}
