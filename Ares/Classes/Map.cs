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
        public List<Player> players = new List<Player>();
        public ClientPlayer clientPlayer;

        public Map(int size)
        {
            clientPlayer = new ClientPlayer();
            tiles = new Tile[size, size];
            Load();
            players.Add(clientPlayer);
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
			for (int i = 0; i < players.Count; i++)
			{
				Player thisPlayer = players[i];
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
					thisTile.Draw();
				}
			}
		}

        private void DrawPlayers()
        {
            for (int i = 0; i < players.Count; i++)
            {
				Player thisPlayer = players[i];
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
            switch (id)
            {
                case 0:
                    tiles[x, y] = new GrassTile(new Vector2f(x, y), UID);
                    break;
                case 1:
                    tiles[x, y] = new WoodWallTile(new Vector2f(x, y), UID);
                    break;
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
            //Console.WriteLine(tiles[(int)(x / 32), (int)(y / 32)].)
            return tiles[(int)(x / 32), (int)(y / 32)];
        }
    }
}
