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
        public bool buildMode = false;
        public float amingAngle = 0;
        public bool noClip = false;

        public ClientPlayer()
            : base()
        {
            Position = new Vector2f(500, 500);
            MovementSpeed = 1.5f;
            DefaultMovementSpeed = MovementSpeed;

            gui = new GUI(this);
            Name = "Player";
        }

        public override void Update()
        {
            //Game.camera2D.Center = Position + new Vector2f(-16, 16);
            setUID();

            Helper.moveCameraTo(Game.camera2D, this.Position + new Vector2f(-16, 16), .15f);
            amingAngle = Helper.AngleBetween(Position + new Vector2f(16, 16), Helper.GetWorldMousePosition());

            gui.Update();

            HandleControls();
            HandleMovement();
            HandleBuilding();

            HandlePositionSending();

            base.Update();
        }

        public override void Draw()
        {
            Render.Draw(Game.charTexture, Position, Color.White, new Vector2f(0, 0), 1, 0);

            Render.Draw(Game.charTexture, new Vector2f(Position.X / 32 * 30 - (Position.Y / 32 * 30) - (30 / 2), Position.Y / 32 * 17 + (Position.X / 32 * 17) - 13 - (17 / 2)), Color.Red, new Vector2f(0, 0), 1, 0f);
            
            Render.DrawString(Game.font, Name, Position - new Vector2f(15, 10), Color.White, .3f, true);
            DrawBuildPreview();
            base.Draw();
        }

        void HandlePositionSending()
        {
            var sinceLastPosSent = DateTime.Now - lastPosSent;
            if (sinceLastPosSent.TotalMilliseconds >= 50)
            {
                sendPos();

                lastPosSent = DateTime.Now;
            }
        }

        void DrawBuildPreview()
        {
            if (buildMode)
            {
                Vector2f prevNorth = new Vector2f(
                    ((int)((Position.X + 16) / 32)) * 32,
                    ((int)((Position.Y + 16) / 32) - 1) * 32
                );
                Render.Draw(Game.wallTexture, prevNorth, new Color(50, 50, 50, 100), new Vector2f(0, 0), 1, 0);

                Vector2f prevEast = new Vector2f(
                    ((int)((Position.X + 16) / 32) + 1) * 32,
                    ((int)((Position.Y + 16) / 32)) * 32
                );
                Render.Draw(Game.wallTexture, prevEast, new Color(50, 50, 50, 100), new Vector2f(0, 0), 1, 0);

                Vector2f prevSouth = new Vector2f(
                    ((int)((Position.X + 16) / 32)) * 32,
                    ((int)((Position.Y + 16) / 32) + 1) * 32
                );
                Render.Draw(Game.wallTexture, prevSouth, new Color(50, 50, 50, 100), new Vector2f(0, 0), 1, 0);

                Vector2f prevWest = new Vector2f(
                    ((int)((Position.X + 16) / 32) - 1) * 32,
                    ((int)((Position.Y + 16) / 32)) * 32
                );
                Render.Draw(Game.wallTexture, prevWest, new Color(50, 50, 50, 100), new Vector2f(0, 0), 1, 0);
            }

        }

        void HandleControls()
        {
            if (Input.isMouseButtonTap(Mouse.Button.Left))
            {
                //Game.map.GameObjects.Add(new Bullet(Position + new Vector2f(-16,16), Helper.AngleBetween(Position + new Vector2f(16,16), Helper.GetWorldMousePosition()), 6,0));

                NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
                outGoingMessage.Write("FIRE");
                outGoingMessage.Write(Position.X - 16);
                outGoingMessage.Write(Position.Y + 16);
                outGoingMessage.Write(amingAngle);
                outGoingMessage.Write(5f);

                Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);
            }

            if (Input.isKeyTap(Keyboard.Key.Q))
            {
                currentBlockType -= 1;
            }

            if (Input.isKeyTap(Keyboard.Key.E))
            {
                currentBlockType += 1;
            }

            if (Input.isKeyTap(Keyboard.Key.N))
            {
                noClip = !noClip;
            }

            if (Input.isKeyTap(Keyboard.Key.B))
            {
                buildMode = !buildMode;
            }

            if (Input.isKeyDown(Keyboard.Key.LShift))
            {
                MovementSpeed = 3;
            }
            else
            {
                MovementSpeed = DefaultMovementSpeed;
            }

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
        }

        void HandleMovement()
        {
            if (noClip || Game.map.getTileInWorld(Position.X + 16 + Velocity.X, Position.Y + 16).Walkable)
                Position.X += Velocity.X;

            if (noClip || Game.map.getTileInWorld(Position.X + 16, Position.Y + 16 + Velocity.Y).Walkable)
                Position.Y += Velocity.Y;
        }

        void HandleBuilding()
        {
            if (Input.isKeyDown(Keyboard.Key.LControl))
            {
                currentBlockType = 0;
            }

            if (buildMode)
            {
                bool didBuild = false;
                //player's standing tile
                Vector2i placePos = new Vector2i((int)(Position.X / 32), (int)(Position.Y / 32));

                if (Input.isKeyTap(Keyboard.Key.Up))
                {
                    placePos.Y--;
                    didBuild = true;
                }
                if (Input.isKeyTap(Keyboard.Key.Left))
                {
                    placePos.X--;
                    didBuild = true;
                }
                if (Input.isKeyTap(Keyboard.Key.Right))
                {
                    placePos.X++;
                    didBuild = true;
                }
                if (Input.isKeyTap(Keyboard.Key.Down))
                {
                    placePos.Y++;
                    didBuild = true;
                }

                if (didBuild)
                {
                    NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
                    outGoingMessage.Write("BUILD");
                    outGoingMessage.Write(placePos.X);
                    outGoingMessage.Write(placePos.Y);
                    outGoingMessage.Write(currentBlockType);

                    Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);
                }
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

        private void setUID()
        {
            if (UID == 0)
                UID = Game.client.UniqueIdentifier;
        }
    }
}
