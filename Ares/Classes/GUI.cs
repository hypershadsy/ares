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
        public enum MenuEnum { inactive, main, inventory, settings};

        public MenuEnum menuEnum = MenuEnum.inactive;
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
            switch (menuEnum)
            {
                case MenuEnum.inactive:
                    break;
                case MenuEnum.inventory:
                    break;
                case MenuEnum.main:
                    break;
                case MenuEnum.settings:
                    break;
            }
        }
    }
}
