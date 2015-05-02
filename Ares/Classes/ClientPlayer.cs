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
            Name = "Cactus Fagtastico";
        }

        public override void Update()
        {
            setUID();

            Helper.moveCameraTo(Game.camera2D, IsoPosition.ToF(), 0.15f);

            gui.Update();

            HandleControls();

            base.Update();
        }

        public override void Draw(float layer)
        {
            IncrementAnimationFrame();
            Vector2f origin = new Vector2f(12f, 55f); //12,55 places his feet approx at the middle of the tile
            //Render.Draw(Game.charTexture, IsoPosition.ToF(), Color.Red, origin, 1, 0f);

            Render.DrawAnimation(Game.idletest, IsoPosition.ToF(), Color.White, origin, 1, 3, 1, frame, 0, layer);

            Render.DrawString(Game.font, Name, IsoPosition.ToF() - new Vector2f(0, 70), Color.Green, 0.3f, true);
        }

        void HandleControls()
        {
            if (Input.isKeyTap(Keyboard.Key.N))
            {
                noClip = !noClip;
            }

            if (Input.isKeyTap(Keyboard.Key.A))
            {
                if (Game.map.getLeftWallInArray(Position.X, Position.Y) == null)// Vector2i(Position.X, Position.Y)
                    sendPos(new Vector2i(Position.X - 1, Position.Y));
                //Position.X--;
            }
            if (Input.isKeyTap(Keyboard.Key.D))
            {
                if (Game.map.getLeftWallInArray(Position.X + 1, Position.Y) == null)
                    sendPos(new Vector2i(Position.X + 1, Position.Y));
                //Position.X++;
            }

            if (Input.isKeyTap(Keyboard.Key.W))
            {
                if (Game.map.getTopWallInArray(Position.X, Position.Y) == null)
                    sendPos(new Vector2i(Position.X, Position.Y - 1));
                //Position.Y--;
            }
            if (Input.isKeyTap(Keyboard.Key.S))
            {
                if (Game.map.getTopWallInArray(Position.X, Position.Y + 1) == null)
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
