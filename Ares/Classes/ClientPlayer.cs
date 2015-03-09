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
        public GUI gui;
        public bool noClip = false;

        public ClientPlayer()
            : base()
        {
            Position = new Vector2i(1, 1);

            gui = new GUI(this);
            Name = "Cactus Fantastico";
        }

        public override void Update()
        {
            setUID();

            Helper.moveCameraTo(Game.camera2D, IsoPosition.ToF(), 0.15f);

            gui.Update();

            HandleControls();

            base.Update();
        }

        public override void Draw()
        {
            IncrementAnimationFrame();
            Vector2f origin = new Vector2f(16f, 40f); //16,40 places his feet approx at the middle of the tile
            //Render.Draw(Game.charTexture, IsoPosition.ToF(), Color.Red, origin, 1, 0f);

            Render.DrawAnimation(Game.idletest, IsoPosition.ToF(), Color.White, origin, 1, 3, 1,ref frame, 1);
            
            Render.DrawString(Game.font, Name, IsoPosition.ToF() - new Vector2f(0, 50), Color.Green, 0.3f, true);

            base.Draw();
        }

        void HandleControls()
        {
            if (Input.isKeyTap(Keyboard.Key.N))
            {
                noClip = !noClip;
            }

            if (Input.isKeyTap(Keyboard.Key.A))
            {
                sendPos(new Vector2i(Position.X - 1, Position.Y));
                //Position.X--;
            }
            if (Input.isKeyTap(Keyboard.Key.D))
            {
                sendPos(new Vector2i(Position.X + 1, Position.Y));
                //Position.X++;
            }

            if (Input.isKeyTap(Keyboard.Key.W))
            {
                sendPos(new Vector2i(Position.X, Position.Y - 1));
                //Position.Y--;
            }
            if (Input.isKeyTap(Keyboard.Key.S))
            {
                sendPos(new Vector2i(Position.X, Position.Y + 1));
                //Position.Y++;
            }
        }


        private void sendPos(Vector2i pos)
        {
            NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
            outGoingMessage.Write("POS");
            outGoingMessage.Write(pos.X);
            outGoingMessage.Write(pos.Y);

            Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);
        }

        private void setUID()
        {
            if (UID == 0)
                UID = Game.client.UniqueIdentifier;
        }
    }
}
