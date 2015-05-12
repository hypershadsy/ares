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
    public class BasicBrownTable : GameObject
    {
        public BasicBrownTable(Vector2i Position)
            : base(Position)
        {
            //Vector2i tilePos = Helper.IsoToTile(Position);
            parentTile = Game.internalGame.map.getTileInArray(Position.X,Position.Y);
        }

        public override void Update()
        {
            parentTile.tCol = Color.Red;
            base.Update();
        }

        public override void Draw()
        {
            var gOrigin = new Vector2f(29,20);
            var gRot = 0f;
            Color gCol = Color.White;
            if (IsoCoords.X / 32 % 2 == 0)
                gCol = new Color(190, 190, 190);
            int gFacing = LeftFacing ? 1 : -1;

            Render.Draw(Game.tableBrown, IsoCoords.ToF(), gCol, gOrigin, gFacing, gRot, .1f);
            base.Draw();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
