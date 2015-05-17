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
        private Tile[, ,] tiles;

        private Wall[, ,] topWalls;
        private Wall[, ,] leftWalls;

        private int floors;

        public List<Actor> Actors = new List<Actor>();
        //public List<NPC> NPCs = new List<(NPC)(); //
        public List<GameObject> GameObjects = new List<GameObject>();
        public ClientPlayer ClientPlayer;
        public float MaxRealY { get; private set; }

        public Map(int size)
        {
            ClientPlayer = new ClientPlayer();
            tiles = new Tile[size, size, floors];
            MaxRealY = Helper.TileToIso(new Vector2i(size - 1, size - 1)).Y;

            topWalls = new Wall[size + 1, size + 1, floors];
            leftWalls = new Wall[size + 1, size + 1, floors];

            Actors.Add(ClientPlayer);
        }


        public void Update()
        {
            UpdateTiles();
            UpdateWalls();
            UpdatePlayers();

            UpdateGameObjects();
        }

        public void Draw()
        {
            DrawTiles();
            DrawWalls();
            DrawPlayers();
            DrawGameObjects();
        }

        private void UpdateTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    for (int z = 0; z < floors; z++)
                    {
                        Tile thisTile = tiles[x, y, z];
                        if (thisTile != null)
                            thisTile.Update();
                    }
                }
            }
        }

        private void UpdateWalls()
        {
            for (int x = 0; x < leftWalls.GetLength(0); x++)
            {
                for (int y = 0; y < leftWalls.GetLength(1); y++)
                {
                    for (int z = 0; z < floors; z++)
                    {
                        Wall thisWall = leftWalls[x, y, z];
                        if (thisWall != null)
                            thisWall.Update();
                    }
                }
            }
            for (int x = 0; x < topWalls.GetLength(0); x++)
            {
                for (int y = 0; y < topWalls.GetLength(1); y++)
                {
                    for (int z = 0; z < floors; z++)
                    {
                        Wall thisWall = topWalls[x, y, z];
                        if (thisWall != null)
                            thisWall.Update();
                    }
                }
            }
        }


        private void UpdatePlayers()
        {
            for (int i = 0; i < Actors.Count; i++)
            {
                Player thisPlayer = (Player)Actors[i];
                thisPlayer.Update();
            }
        }

        private void DrawTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    for (int z = 0; z < floors; z++)
                    {
                        Tile thisTile = tiles[x, y, z];
                        if (thisTile != null)
                            thisTile.Draw();
                    }
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
                    for (int z = 0; z < floors; z++)
                    {
                        Wall thisLeftWall = leftWalls[x, y, z];
                        Wall thisTopWall = topWalls[x, y, z];
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
        }

        private void DrawPlayers()
        {
            for (int i = 0; i < Actors.Count; i++)
            {
                Player thisPlayer = (Player)Actors[i];
                //it's -1 because there's a corner case with topWall directly down, leftWall directly right
                int thisRealY = thisPlayer.IsoPosition.Y - 1;
                float lerpVal = thisRealY / (float)MaxRealY;
                float layer = Helper.Lerp(Layer.WallFar, Layer.WallNear, lerpVal);
                thisPlayer.Draw(layer);
            }
        }

        private void DrawGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject thisGameObject = GameObjects[i];
                thisGameObject.Draw();
            }
        }

        private void UpdateGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject thisGameObject = GameObjects[i];
                thisGameObject.Update();
            }
        }

        /// <summary>
        /// Used to add a tile at an array index, not world coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        public void addTile(int x, int y, int z, int type)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        tiles[x, y, z] = new WoodTile(new Vector2i(x, y));
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
        public void addWall(int x, int y, int z, int type, bool leftFacing)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        if (leftFacing)
                            leftWalls[x, y, z] = new RedBrickWall(new Vector2i(x, y), true);
                        else
                            topWalls[x, y, z] = new RedBrickWall(new Vector2i(x, y), false);
                        break;
                    case 1:
                        if (leftFacing)
                            leftWalls[x, y, z] = new WoodDoor(new Vector2i(x, y), true);
                        else
                            topWalls[x, y, z] = new WoodDoor(new Vector2i(x, y), false);
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        public Tile getTileInArray(int x, int y, int z)
        {
            return tiles[x, y, z];
        }

        /// <summary>
        /// Used to get a tile at a world coordinate, not an array index
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile getTileInWorld(float x, float y, int z)
        {
            return tiles[(int)(x / 32), (int)(y / 32), z];
        }

        public Wall getTopWallInArray(int x, int y, int z)
        {
            try
            {
                return topWalls[(int)(x), (int)(y), z];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public Wall getLeftWallInArray(int x, int y, int z)
        {
            try
            {
                return leftWalls[(int)(x), (int)(y), z];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
    }
}
