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
    public class ClientPlayer : Player
    {
        Sprite characterSprite;

        public ClientPlayer()
            : base()
        {
            characterSprite = new Sprite(Game.charTexture);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            Render.Draw(characterSprite, Position, Color.White, new Vector2f(0, 0), 1);
            base.Draw();
        }

        void HandleMovement()
        {

        }
    }
}
