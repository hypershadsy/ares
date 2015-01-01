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
            speed = 3;
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
            Velocity = new Vector2f(0, 0);

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

            Position += Velocity * Game.getDeltaRatio();
        }

        public void sendChat()
        {
            NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
            outGoingMessage.Write("CHAT");
            outGoingMessage.Write(textCapture);
            Game.chatMessages.Add("You: " + textCapture);
            //SoundPlayer.playSound(Game.click);
            Game.soundInstances.Add(new SoundInstance(Game.click, 0f, 0f));
            if (Game.chatMessages.Count > 15)
                Game.chatMessages.RemoveAt(0);

            if (!(textCapture.IndexOf("/") == 0))
                overheadMessage = textCapture;
            ohmDecay = (60 * 5);

            if (textCapture.IndexOf("/setname") == 0)
                if (textCapture.Substring(0, 8).Equals("/setname"))
                {
                    username = textCapture.Substring(8).Trim();
                }

            if (textCapture.IndexOf("/scream") == 0)
                Game.soundInstances.Add(new SoundInstance(Game.SaD, 0f, 0f));

            if (textCapture.IndexOf("/fart") == 0)
                Game.soundInstances.Add(new SoundInstance(Game.fart, 0f, 0f));
            if (textCapture.IndexOf("/clear") == 0)
                Game.chatMessages.Clear();
            if (textCapture.IndexOf("/kill") == 0)
            {
                alive = false;
                sendAliveStatus();
                Game.soundInstances.Add(new SoundInstance(Game.fart, 0f, 0f));
                Game.soundInstances.Add(new SoundInstance(Game.SaD, 0f, 0f));
            }


            textCapture = "";
            Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
