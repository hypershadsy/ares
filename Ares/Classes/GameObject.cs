﻿using System;
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
        public Vector2i Position;
        public float DrawLayer;
        public bool LeftFacing = false;
        public long OwnerUID;
        private int MaxRealY;

        public Tile parentTile;

        public Vector2i IsoCoords
        {
            get
            {
                return Helper.TileToIso(Position);
            }
        }
        public Vector2i IsoPosition
        {
            get
            {
                Vector2i ret = Helper.TileToIso(Position);
                ret.Y += 16;
                return ret; //isospace pos of feet standing at center of tile
            }
        }

        public GameObject()
        {
        }

        public GameObject(Vector2i position)
        {
            this.Position = position;
        }

        public virtual void Update()
        {
            DrawLayer = 0;
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
