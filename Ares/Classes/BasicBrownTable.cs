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
            this.Position = Position;
        }

        public override void Update()
        {
            
            base.Update();
        }

        public override void Draw()
        {
            var tOrigin = new Vector2f(32f, 47f);
            var tRot = 0f;
            Color tCol = Color.White;
            if (IsoCoords.X / 32 % 2 == 0)
                tCol = new Color(190, 190, 190);
            int tFacing = LeftFacing ? 1 : -1;
            Render.Draw(Game.tableBrown, IsoCoords.ToF(), tCol, tOrigin, tFacing, tRot, 0);
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
