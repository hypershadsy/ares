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
    public class Chat
    {
        public List<ChatMessage> messages = new List<ChatMessage>() { };
        public string ChatDraft = "";
        public bool DraftEditorOpen = false;
        public float chatScale = .5f;
        public Player PlayerSender;
        public int lineDisplayCount = 15;

        public Chat(Player PlayerSender)
        {
            this.PlayerSender = PlayerSender;
            //Game.window.TextEntered += (object sender, TextEventArgs e) =>
            //{
            //    if (DraftEditorOpen)
            //    {
            //        if (Keyboard.IsKeyPressed(Keyboard.Key.Back))
            //        {
            //            if (ChatDraft.Length > 0)
            //                ChatDraft = ChatDraft.Substring(0, ChatDraft.Length - 1);
            //        }
            //        else if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            //        {
            //        }
            //        else if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
            //        {
            //        }
            //        else if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
            //        {
            //        }
            //        else
            //        {
            //            ChatDraft += e.Unicode;
            //        }
            //    }
            //};
        }

        public void Update()
        {
            if (Input.isKeyTap(Keyboard.Key.Return))
            {
                if (ChatDraft.IndexOf("/setname") == 0)
                {
                    if (ChatDraft.Substring(0, 8).Equals("/setname"))
                    {
                        string username = ChatDraft.Substring(8).Trim();

                        NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
                        outGoingMessage.Write("NAME");
                        outGoingMessage.Write(username);
                        Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);

                        ChatDraft = "";
                    }
                }
                else
                    if (!ChatDraft.Trim().Equals(""))
                    {
                        //messages.Add(new ChatMessage(ChatDraft, PlayerSender));

                        NetOutgoingMessage outGoingMessage = Game.client.CreateMessage();
                        outGoingMessage.Write("CHAT");
                        outGoingMessage.Write(ChatDraft);
                        Game.client.SendMessage(outGoingMessage, NetDeliveryMethod.ReliableOrdered);

                        ChatDraft = "";
                    }

                DraftEditorOpen = !DraftEditorOpen;
            }
        }

        public void Draw()
        {
            Render.DrawString(Game.font, ChatDraft, new Vector2f(0, 0), Color.White, chatScale, false);
            if (DraftEditorOpen)
            {
                Render.DrawString(Game.font, "|", new Vector2f((18 * chatScale) * ChatDraft.Length, 0), Color.White, chatScale, false); //add chat cursor
            }

            if (messages.Count > lineDisplayCount)
                messages.RemoveAt(0);

            for (int i = 0; i < messages.Count; i++)
            {
                Render.DrawString(Game.font, messages[i].Message, new Vector2f(0, 20 + (i * 15)), Color.White, chatScale, false);
            }
        }
    }

    public class ChatMessage
    {
        public Player Sender { get; private set; }
        public string Message { get; private set; }

        public ChatMessage(string msg, Player sender)
        {
            this.Message = msg;
            this.Sender = sender;
        }
    }
}
