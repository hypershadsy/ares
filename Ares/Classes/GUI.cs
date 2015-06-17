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
            if (!menuEnum.Equals(MenuEnum.inactive))
            {
                Render.Draw(Game.menu, new Vector2f(30, 10), Color.White, new Vector2f(0, 0), 1, 0, 0f);
                Render.Draw(Game.invIcon, new Vector2f(71, 144), new Color(60,60,60), new Vector2f(0, 0), 1, 0, 0f);
                Render.DrawString(Game.font,"Inventory", new Vector2f(101, 144), new Color(60, 60, 60),.7f,false, 0f);

                Render.Draw(Game.checIcon, new Vector2f(73, 190), new Color(60, 60, 60), new Vector2f(0, 0), 1, 0, 0f);
                Render.DrawString(Game.font, "Task List", new Vector2f(101, 190), new Color(60, 60, 60), .7f, false, 0f);

                Render.Draw(Game.settIcon, new Vector2f(73, 239), new Color(60, 60, 60), new Vector2f(0, 0), 1, 0, 0f);
                Render.DrawString(Game.font, "Settings", new Vector2f(101, 239), new Color(60, 60, 60), .7f, false, 0f);

                Render.DrawString(Game.font, "Menu", new Vector2f(125, 84), Color.White, 1f, false, 0f);

                for (int i = 0; i < 10; i++)
                {
                    Render.Draw(Game.pixel, new Vector2f(230  + (i * 50), 144), Color.White, new Vector2f(0, 0), 1, 0, 0);
                }
            }
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
            Render.DrawString(Game.font, Mouse.GetPosition(Game.window).ToString(), new Vector2f(10, 47), Color.White, .5f, false, 0f); 
        }
    }
}
