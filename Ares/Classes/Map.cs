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

        public Map(int size)
        {
            tiles = new Tile[size, size];
            Load();
            players.Add(new ClientPlayer());
        }

        private void Load()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y] = new GroundTile(new Vector2f(x, y));
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
    }
}
