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
    public class WoodDoor : Door
    {
        public WoodDoor(Vector2i position, bool leftFacing)
            : base(position, leftFacing)
        {
        }

        public override void Update()
        {
            for (int i = 0; i < Game.internalGame.map.Players.Count; i++)
            {
                Actor iActor = Game.internalGame.map.Players[i]; // This will need to refer to NPCs as well
                if (Helper.Distance(iActor.Position, this.Position + new Vector2i(20, 53)) < 35)
                {
                    open = true;
                }
                else
                    open = false;
            }

            if (Helper.Distance(Game.internalGame.map.ClientPlayer.IsoPosition, this.Position + new Vector2i(20, 53)) < 35)
            {
                open = true;
            }
            else
                open = false;

            Console.WriteLine(Helper.Distance(Game.internalGame.map.ClientPlayer.IsoPosition, this.Position + new Vector2i(20, 53)));

            base.Update();
        }

        public override void Draw(float drawLayer)
        {
            var tOrigin = new Vector2f(32f, 47f);
            var tRot = 0f;
            Color tCol = Color.White;
            if (IsoCoords.X / 32 % 2 == 0)
                tCol = new Color(190, 190, 190);
            int tFacing = open ? 1 : -1;
            Render.Draw(Game.woodDoor1, IsoCoords.ToF(), tCol, tOrigin, tFacing, tRot, drawLayer);

            base.Draw(drawLayer);
        }
    }
}
