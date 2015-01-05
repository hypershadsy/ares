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
    public class GUI
    {
        public Chat chat;
        public Player attachedPlayer;

        public GUI(Player player)
        {
            attachedPlayer = player;
            chat = new Chat(player);
        }

        public void Update()
        {
            chat.Update();
        }
        public void Draw()
        {
            chat.Draw();
            if (((ClientPlayer)attachedPlayer).buildMode)
            {
                Render.DrawString(Game.font, "BUILDING", new Vector2f(Game.window.Size.X - 100, 10), Color.White, .5f, false);
            }
        }
    }

}
