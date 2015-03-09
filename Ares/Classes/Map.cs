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

        public List<Player> Players = new List<Player>();
        public List<GameObject> GameObjects = new List<GameObject>();
        public ClientPlayer ClientPlayer;

        public Map(int size)
        {
            ClientPlayer = new ClientPlayer();
            tiles = new Tile[size, size];

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
                    tiles[x, y] = new FloorTile(new Vector2i(x, y), -1);
                }
            }

            for (int x = 0; x < topWalls.GetLength(0); x++)
            {
                for (int y = 0; y < topWalls.GetLength(1); y++)
                {
                    topWalls[x, y] = null;
                }
            }
            for (int x = 0; x < leftWalls.GetLength(0); x++)
            {
                for (int y = 0; y < leftWalls.GetLength(1); y++)
                {
                    leftWalls[x, y] = null;
                }
            }
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

        private void DrawPlayers()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Player thisPlayer = Players[i];
                thisPlayer.Draw();
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
        public void addTile(int x, int y, int id, long UID)
        {
            try
            {
                switch (id)
                {
                    case 0:
                        tiles[x, y] = new FloorTile(new Vector2i(x, y), UID);
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
    }
}
