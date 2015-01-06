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
    public class Projectile : GameObject
    {
        

        public Projectile()
            : base()
        {
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public virtual void Move()
        {
            
        }
    }
}
