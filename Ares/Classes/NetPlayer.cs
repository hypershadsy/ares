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
    public class NetPlayer : Player
    {
        public Vector2f PositionInter;

        public NetPlayer(Map currentMap, long uid)
        {
            this.UID = uid;
            this.currentMap = currentMap;
            Position = new Vector2i(1, 1);
            PositionInter = IsoPosition.ToF();
            Name = "Cactus Fantastico";
        }

        public override void Update()
        {
            Vector2f diff = PositionInter - IsoPosition.ToF();
            PositionInter = IsoPosition.ToF() + (diff / 2f);
        }

        public override void Draw(float layer)
        {
            //Vector2f origin = new Vector2f(16f, 28f);
            //Render.Draw(Game.charTexture, PositionInter, Color.Yellow, origin, 1, 0);
            //Render.DrawString(Game.font, Name, PositionInter - new Vector2f(15, 10), Color.White, .3f, true);

            IncrementAnimationFrame();
            Vector2f origin = new Vector2f(12f, 55f); //12,55 places his feet approx at the middle of the tile

            Render.DrawAnimation(Game.idletest, IsoPosition.ToF(), Color.White, origin, 1, 3, 1, frame, 0, layer);

            Render.DrawString(Game.font, Name, IsoPosition.ToF() - new Vector2f(0, 50), Color.Green, 0.3f, true);
        }
    }
}
