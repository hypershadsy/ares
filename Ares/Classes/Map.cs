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
        public List<Player> Players = new List<Player>();
        public List<GameObject> GameObjects = new List<GameObject>();
        public ClientPlayer ClientPlayer;

        public Map(int size)
        {
            ClientPlayer = new ClientPlayer();
            tiles = new Tile[size, size];
            Load();
            Players.Add(ClientPlayer);
        }

        private void Load()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y] = new GrassTile(new Vector2f(x, y), -1);
                }
            }
        }

        public void Update()
        {
            UpdateTiles();
            UpdatePlayers();
        }

        public void Draw()
        {
            DrawTiles();
            DrawPlayers();
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
                    if (Helper.Distance(thisTile.Position * 32, ClientPlayer.Position) < 300) //Tiles are not drawn if they are too far away, 
                        thisTile.Draw();                                                      //however they will still be updated
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
                        tiles[x, y] = new GrassTile(new Vector2f(x, y), UID);
                        break;
                    case 1:
                        tiles[x, y] = new WoodWallTile(new Vector2f(x, y), UID);
                        break;
                    case 2:
                        tiles[x, y] = new DoorTile(new Vector2f(x, y), UID);
                        break;
                    case 3:
                        tiles[x, y] = new PathTile(new Vector2f(x, y), UID);
                        break;
                }
            }
            catch (Exception) { }
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
            //Console.WriteLine(tiles[(int)(x / 32), (int)(y / 32)].)
            return tiles[(int)(x / 32), (int)(y / 32)];
        }
    }
}
