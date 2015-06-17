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
    public class Package : GameObject
    {
        public Package(Vector3i position, int UID, bool leftFacing)
            : base(position, UID, leftFacing)
        {
            texture = Content.GetTexture("furniture/package.png");
            interactable = true;
        }

        public override void Update()
        {
            if (Input.isKeyTap(Keyboard.Key.E))
                Activate();
            base.Update();
        }

        public override void Draw()
        {
            if (Helper.Distance(Helper.V3itoVec2f(Position), Helper.V3itoVec2f(Game.internalGame.map.ClientPlayer.Position)) < 1)
            {
                Texture interactButton = Content.GetTexture("gui/interactButton.png");
                Render.Draw(interactButton, IsoCoords.ToF() - new Vector2f(0,30), Color.White, new Vector2f(0, 0), 1, 0, 0);
                Render.DrawString(Content.GetFont("Font1.ttf"), "Pick Up", IsoCoords.ToF() - new Vector2f(-45, 29), Color.White,.4f,true, 0);
            
            }

            Render.Draw(texture, IsoCoords.ToF(), Color.White, new Vector2f(0, 0), 1, 0, Helper.TilePosToLayer(new Vector2i(Position.X,Position.Y)));
            base.Draw();
        }

        public override void Activate()
        {
            Game.internalGame.map.ClientPlayer.inventory.Add(this);
            Game.internalGame.map.GameObjects.Remove(this);
            base.Activate();
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

   
}
