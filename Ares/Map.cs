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
                    tiles[x, y] = new GroundTile();
                }
            }
        }

        public void Update()
        {

        }

        public void Draw()
        {
            DrawPlayers();
        }

        public void DrawPlayers()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Draw();
            }
        }

        public void DrawTiles()
        {
        }
    }
}
