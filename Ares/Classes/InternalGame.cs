using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace Ares
{
    public class InternalGame : GameState
    {
        public Map map;

        public override void Update()
        {
            base.Update();
            map.Update();
        }

        public override void Draw()
        {
            base.Draw();
            Game.window.SetView(Game.camera2D);
            map.Draw();
            Render.Draw(Game.cityBackground, new Vector2f(0, 0), Color.White, new Vector2f(1066, 818), 1, 0f, 1);
            Render.SpitToWindow();

            Game.window.SetView(Game.window.DefaultView);
            map.ClientPlayer.gui.Draw();
            Render.SpitToWindow();
        }
    }
}
