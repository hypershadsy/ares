using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ares
{
    public abstract class GameState
    {
        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }
    }
}
