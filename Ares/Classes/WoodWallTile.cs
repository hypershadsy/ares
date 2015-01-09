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
    public class WoodWallTile : WallTile
    {
        public WoodWallTile(Vector2f position, long UID_Builder)
            : base(position, UID_Builder)
        {
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            Render.Draw(Game.wallTexture, Position * 32, Color.White, new Vector2f(0, 0), 1, 0);
            
            Render.Draw(Game.isoBlock, new Vector2f(Position.X * 30 - (Position.Y * 30), Position.Y * 17 + (Position.X * 17) - 13), Color.Red, new Vector2f(0, 0), 1, 0f);
            Render.Draw(Game.isoBlock, new Vector2f(Position.X * 30 - (Position.Y * 30), Position.Y * 17 + (Position.X * 17) - 26), Color.Red, new Vector2f(0, 0), 1, 0f);
            
            //Render.Draw(Game.isoBlock, new Vector2f(Position.X * 60 / 2 - (Position.Y * 31), Position.Y * 17 + (Position.X * 17) - 26), Color.Red, new Vector2f(0, 0), 1, 0f);
             
        }
    }
}
