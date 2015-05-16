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
    public class Map
    {
        private Tile[,] tiles;

        private Wall[,] topWalls;
        private Wall[,] leftWalls;

        public float MaxRealY { get; private set; }
        public int floor;

        public Map(int size, int floor)
        {
            this.floor = floor;
            tiles = new Tile[size, size];
            MaxRealY = Helper.TileToIso(new Vector2i(size - 1, size - 1)).Y;

            topWalls = new Wall[size + 1, size + 1];
            leftWalls = new Wall[size + 1, size + 1];

        }


        public void Update()
        {
            UpdateTiles();
            UpdateWalls();
        }

        public void Draw()
        {
            DrawTiles();
            DrawWalls();
        }

        private void UpdateTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Tile thisTile = tiles[x, y];
                    if (thisTile != null)
                        thisTile.Update();
                }
            }
        }

        private void UpdateWalls()
        {
            for (int x = 0; x < leftWalls.GetLength(0); x++)
            {
                for (int y = 0; y < leftWalls.GetLength(1); y++)
                {
                    Wall thisWall = leftWalls[x, y];
                    if (thisWall != null)
                        thisWall.Update();
                }
            }
            for (int x = 0; x < topWalls.GetLength(0); x++)
            {
                for (int y = 0; y < topWalls.GetLength(1); y++)
                {
                    Wall thisWall = topWalls[x, y];
                    if (thisWall != null)
                        thisWall.Update();
                }
            }
        }

        private void DrawTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Tile thisTile = tiles[x, y];
                    if (thisTile != null)
                        thisTile.Draw();
                }
            }
        }

        private void DrawWalls() //Not for future use: Must be rewritten for draw order
        {
            var width = leftWalls.GetLength(0);
            var height = leftWalls.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Wall thisLeftWall = leftWalls[x, y];
                    Wall thisTopWall = topWalls[x, y];
                    var layer = Helper.TilePosToLayer(new Vector2i(x, y));

                    //TODO: draw distance
                    if (thisLeftWall != null)
                    {
                        thisLeftWall.Draw(layer);
                    }
                    if (thisTopWall != null)
                    {
                        thisTopWall.Draw(layer);
                    }
                }
            }
        }

        /// <summary>
        /// Used to add a tile at an array index, not world coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        public void addTile(int x, int y, int type)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        tiles[x, y] = new WoodTile(new Vector2i(x, y));
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Used to get a wall at an array index, not world coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        public void addWall(int x, int y, int type, bool leftFacing)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        if (leftFacing)
                            leftWalls[x, y] = new RedBrickWall(this, new Vector2i(x, y), true);
                        else
                            topWalls[x, y] = new RedBrickWall(this, new Vector2i(x, y), false);
                        break;
                    case 1:
                        if (leftFacing)
                            leftWalls[x, y] = new WoodDoor(this, new Vector2i(x, y), true);
                        else
                            topWalls[x, y] = new WoodDoor(this, new Vector2i(x, y), false);
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        public Tile getTileInArray(int x, int y)
        {
            return tiles[x, y];
        }

        /// <summary>
        /// Used to get a tile at a world coordinate, not an array index
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile getTileInWorld(float x, float y)
        {
            return tiles[(int)(x / 32), (int)(y / 32)];
        }

        public Wall getTopWallInArray(int x, int y)
        {
            try
            {
                return topWalls[(int)(x), (int)(y)];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public Wall getLeftWallInArray(int x, int y)
        {
            try
            {
                return leftWalls[(int)(x), (int)(y)];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
    }
}
