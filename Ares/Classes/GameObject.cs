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
    public abstract class GameObject
    {
        public Vector3i Position;
        public bool LeftFacing = false;
        public long UID;
        public Texture texture;
        public bool interactable;


        public Tile parentTile;

        public Vector2i IsoCoords
        {
            get
            {
                return Helper.TileToIso(new Vector2i(Position.X,Position.Y));
            }
        }

        public GameObject()
        {
        }

        public GameObject(Vector3i position, int UID, bool leftFacing)
        {
            this.Position = position;
            this.LeftFacing = leftFacing;
            this.UID = UID;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {

        }

        public virtual void Activate()
        {
        }

        public virtual void Destroy()
        {
            Game.internalGame.map.GameObjects.Remove(this);
        }
    }
}
