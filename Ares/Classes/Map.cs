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
        private int MaxRealY;

        public List<Player> Players = new List<Player>();
        public List<GameObject> GameObjects = new List<GameObject>();
        public ClientPlayer ClientPlayer;

        public Map(int size)
        {
            ClientPlayer = new ClientPlayer();
            tiles = new Tile[size, size];
            MaxRealY = Helper.TileToIso(new Vector2i(size - 1, size - 1)).Y;

            topWalls = new Wall[size + 1, size + 1];
            leftWalls = new Wall[size + 1, size + 1];

            Load();
            Players.Add(ClientPlayer);
        }

        private void Load()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y] = new WoodTile(new Vector2i(x, y));
                }
            }

            HARDCODEWALLS();
        }

        private void HARDCODEWALLS()
        {
            for (int y = 0; y < 20; y++) //left side
            {
                leftWalls[0, y] = new RedBrickWall(new Vector2i(0, y), true);
            }

            for (int y = 0; y < 20; y++) //right side
            {
                leftWalls[20, y] = new RedBrickWall(new Vector2i(20, y), true);
            }

            for (int x = 0; x < 20; x++) //top side
            {
                topWalls[x, 0] = new RedBrickWall(new Vector2i(x, 0), false);
            }
            for (int x = 0; x < 20; x++) //top side
            {
                topWalls[x, 20] = new RedBrickWall(new Vector2i(x, 20), false);
            }

            topWalls[10, 10] = new RedBrickWall(new Vector2i(10, 10), false);
            topWalls[10, 11] = new RedBrickWall(new Vector2i(10, 11), false);
            leftWalls[10, 10] = new RedBrickWall(new Vector2i(10, 10), true);
        }

        public void Update()
        {
            UpdateTiles();
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
                    Tile thisTile = tiles[x, y];
                    thisTile.Update();
                }
            }
        }

        private void UpdatePlayers()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Player thisPlayer = Players[i];
                thisPlayer.Update();
            }
        }

        private void DrawTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Tile thisTile = tiles[x, y];
                    //if (Helper.Distance(thisTile.Position * 32, ClientPlayer.Position) < 300) //draw distance
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
                    int thisRealY = Helper.TileToIso(new Vector2i(x, y)).Y;
                    float lerpVal = thisRealY / (float)MaxRealY;
                    float layer = Helper.Lerp(Layer.WallFar, Layer.WallNear, lerpVal);

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

        private void DrawPlayers()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Player thisPlayer = Players[i];
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
        /// Used to get a tile at an array index, not world coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="id"></param>
        public void addTile(int x, int y, int id)
        {
            try
            {
                switch (id)
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
