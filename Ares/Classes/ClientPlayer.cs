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
        DateTime lastPosSent;

        public ClientPlayer()
            : base()
        {
            Position = new Vector2f(100, 100);
            MovementSpeed = 3;
        }

        public override void Update()
        {
            HandleMovement();
            var sinceLastPosSent = DateTime.Now - lastPosSent;
            if (sinceLastPosSent.TotalMilliseconds >= 50)
            {
                //sehdpos

                lastPosSent = DateTime.Now;
            }
            base.Update();
        }

        public override void Draw()
        {
            Render.Draw(Game.charTexture, Position, Color.White, new Vector2f(0, 0), 1);
            base.Draw();
        }

        void HandleMovement()
        {
            Velocity = new Vector2f(0, 0); //Reset the velocity

            if (Input.isKeyDown(Keyboard.Key.A))
            {
                Velocity.X = -MovementSpeed;
            }
            if (Input.isKeyDown(Keyboard.Key.D))
            {
                Velocity.X = MovementSpeed;
            }

            if (Input.isKeyDown(Keyboard.Key.W))
            {
                Velocity.Y = -MovementSpeed;
            }
            if (Input.isKeyDown(Keyboard.Key.S))
            {
                Velocity.Y = MovementSpeed;
            }

            var delta = Game.getDeltaRatio();
            Position += Velocity * delta;            
        }

        private void sendPos()
        {

            NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
            outGoingMessage.Write("POS");
            outGoingMessage.Write(Position.X);
            outGoingMessage.Write(Position.Y);

            Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.Unreliable);
        }
    }
}
