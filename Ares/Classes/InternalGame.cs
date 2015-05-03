using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            map.Draw();
        }
    }
}
