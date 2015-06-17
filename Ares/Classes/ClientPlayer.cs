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
        public List<GameObject> inventory = new List<GameObject>() { };

        public ClientPlayer()
            : base()
        {
            Position = new Vector3i(1, 1, 0);

            gui = new GUI(this);
            Name = "Seymour Butts";
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

            Texture idleTest = Content.GetTexture("debug/idletest.png");
            Render.DrawAnimation(idleTest, IsoPosition.ToF(), Color.White, origin, 1, 3, 1, frame, 0, layer);

            Font font = Content.GetFont("Font1.ttf");
            Render.DrawString(font, Name, IsoPosition.ToF() - new Vector2f(0, 70), Color.Green, 0.3f, true);
        }

        void HandleControls()
        {
            if (Input.isKeyTap(Keyboard.Key.I))
            {
                if (gui.menuEnum.Equals(GUI.MenuEnum.inactive))
                    gui.menuEnum = GUI.MenuEnum.inventory;
                else if (gui.menuEnum.Equals(GUI.MenuEnum.inventory))
                    gui.menuEnum = GUI.MenuEnum.inactive;
            }

            if (Input.isKeyTap(Keyboard.Key.B)) //Add a door
            {
                Game.internalGame.map.AddWall(this.Position.X, this.Position.Y, this.Position.Z, 1, true);
            }
            if (Input.isKeyTap(Keyboard.Key.V)) //Add a door
            {
                Game.internalGame.map.AddWall(this.Position.X, this.Position.Y, this.Position.Z, 1, false);
            }
            if (Input.isKeyTap(Keyboard.Key.Up)) //Add a door
            {
                Position.Z++;
            }
            if (Input.isKeyTap(Keyboard.Key.Down)) //Add a door
            {
                Position.Z--;
            }

            if (Input.isKeyTap(Keyboard.Key.A))
            {
                Wall wall = Game.internalGame.map.GetWallLeft(Position.X, Position.Y, Position.Z);
                if (wall == null ||
                    (wall is Door && ((Door)wall).open))// Vector2i(Position.X, Position.Y)
                    sendPos(new Vector3i(Position.X - 1, Position.Y, Position.Z));
                //Position.X--;
            }
            if (Input.isKeyTap(Keyboard.Key.D))
            {
                Wall wall = Game.internalGame.map.GetWallLeft(Position.X + 1, Position.Y, Position.Z);
                if (wall == null ||
                    (wall is Door && ((Door)wall).open))
                    sendPos(new Vector3i(Position.X + 1, Position.Y, Position.Z));
                //Position.X++;
            }

            if (Input.isKeyTap(Keyboard.Key.W))
            {
                Wall wall = Game.internalGame.map.GetWallTop(Position.X, Position.Y, Position.Z);
                if (wall == null ||
                (wall is Door && ((Door)wall).open))
                    sendPos(new Vector3i(Position.X, Position.Y - 1, Position.Z));
                //Position.Y--;
            }
            if (Input.isKeyTap(Keyboard.Key.S))
            {
                Wall wall = Game.internalGame.map.GetWallTop(Position.X, Position.Y + 1, Position.Z);
                if (wall == null ||
                (wall is Door && ((Door)wall).open))
                    sendPos(new Vector3i(Position.X, Position.Y + 1, Position.Z));
                //Position.Y++;
            }
        }

        private void sendPos(Vector3i pos)
        {
            NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
            outGoingMessage.Write("POS");
            outGoingMessage.Write(pos.X);
            outGoingMessage.Write(pos.Y);
            outGoingMessage.Write(pos.Z);
            Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);
        }

        private void setUID()
        {
            if (UID == 0)
                UID = Game.client.UniqueIdentifier;
        }
    }
}
