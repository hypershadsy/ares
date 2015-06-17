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
        public enum MenuEnum { inactive, main, inventory, settings };

        public MenuEnum menuEnum = MenuEnum.inactive;
        public Chat chat;
        public ClientPlayer attachedPlayer;

        public GUI(ClientPlayer player)
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
            Font font = Content.GetFont("Font1.ttf");
            chat.Draw();
            if (!menuEnum.Equals(MenuEnum.inactive))
            {
                Render.Draw(Content.GetTexture("gui/menu.png"), new Vector2f(210, 150), Color.White, new Vector2f(0, 0), 1, 0, 0f);
            }

            switch (menuEnum)
            {
                case MenuEnum.inactive:
                    break;
                case MenuEnum.inventory:
                    for (int i = 0; i < 8; i++)
                    {
                        Render.Draw(Content.GetTexture("pixel.png"), new Vector2f(310 + (i * 28), 244), Color.White, new Vector2f(0, 0), 1, 0, 0, 25);
                        if (i < attachedPlayer.inventory.Count && attachedPlayer.inventory[i] != null)
                        {
                            Texture t = attachedPlayer.inventory[i].texture;
                            Render.Draw(t, new Vector2f(310 + (i * 25) + 13 - t.Size.X / 2, 244 + 12 - t.Size.Y / 2), Color.White, new Vector2f(0, 0), 1, 0f, 0, 1f);
                        }
                    }
                    break;
                case MenuEnum.main:
                    break;
                case MenuEnum.settings:
                    break;
            }
            Render.DrawString(font, Mouse.GetPosition(Game.window).ToString(), new Vector2f(210, 150), Color.White, .25f, false, 0f);
        }
    }
}
