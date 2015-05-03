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
            map.Update();
            base.Update();
        }

        public override void Draw()
        {
            map.Draw();
            base.Draw();
        }
    }
}
