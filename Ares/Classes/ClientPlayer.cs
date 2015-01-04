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
        public GUI gui;
        public int currentBlockType = 1;

        public ClientPlayer()
            : base()
        {
            Position = new Vector2f(100, 100);
            MovementSpeed = 3;
            UID = Game.client.UniqueIdentifier;
            gui = new GUI(this);
        }

        public override void Update()
        {
            Game.camera2D.Center = Position;
            gui.Update();

            HandleMovement();
            HandleBuilding();

            var sinceLastPosSent = DateTime.Now - lastPosSent;
            if (sinceLastPosSent.TotalMilliseconds >= 50)
            {
                sendPos();

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

            //if (Game.map.getTileInWorld(Position.X + Velocity.X, Position.Y) is GroundTile)
                Position.X += Velocity.X;

            //if (Game.map.getTileInWorld(Position.X, Position.Y + Velocity.Y) is GroundTile)
                Position.Y += Velocity.Y;
        }

        void HandleBuilding()
        {
            if (Input.isKeyTap(Keyboard.Key.Up))
            {
                Vector2i pos = new Vector2i((int)((Position.X + 16) / 32), (int)((Position.Y + 16) / 32) - 1); //One block above

                NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
                outGoingMessage.Write("BUILD");
                outGoingMessage.Write(pos.X);
                outGoingMessage.Write(pos.Y);
                outGoingMessage.Write(currentBlockType);

                Game.map.addTile(pos.X, pos.Y, currentBlockType, Game.client.UniqueIdentifier);

                Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);
            }
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
