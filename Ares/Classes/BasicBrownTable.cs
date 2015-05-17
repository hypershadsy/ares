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
        public BasicBrownTable(Vector3i Position, int UID, bool leftFacing)
            : base(Position, UID, leftFacing)
        {
            //Vector2i tilePos = Helper.IsoToTile(Position);
            parentTile = Game.internalGame.map.getTileInArray(Position.X, Position.Y, Position.Z);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            var gOrigin = new Vector2f(29,20);
            var gRot = 0f;
            int gFacing = LeftFacing ? 1 : -1;
            float gLayer = Helper.TilePosToLayer(new Vector2i(Position.X,Position.Y));

            Render.Draw(Game.tableBrown, IsoCoords.ToF(), Color.White, gOrigin, gFacing, gRot, gLayer);
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
