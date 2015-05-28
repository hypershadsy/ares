using System;
using System.Collections.Generic;
using SFML.Window;

namespace Ares
{
    public class Map
    {
        private Tile[,,] tiles;
        private Wall[,,] topWalls;
        private Wall[,,] leftWalls;

        private const int FLOORS = 20;
        public const int wallSpriteHeight = 45;

        public List<Actor> Actors { get; private set; }
        public List<GameObject> GameObjects { get; private set; }
        public ClientPlayer ClientPlayer { get; private set; }
        public float MaxRealY { get; private set; }

        public Map(int size)
        {
            tiles = new Tile[size, size, FLOORS];
            topWalls = new Wall[size + 1, size + 1, FLOORS];
            leftWalls = new Wall[size + 1, size + 1, FLOORS];

            Actors = new List<Actor>();
            GameObjects = new List<GameObject>();
            ClientPlayer = new ClientPlayer();
            MaxRealY = Helper.TileToIso(new Vector2i(size - 1, size - 1)).Y;

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
            DrawLowerFloors();
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
                    for (int z = 0; z < FLOORS; z++)
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
                    for (int z = 0; z < FLOORS; z++)
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
                    for (int z = 0; z < FLOORS; z++)
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

        private void UpdateGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject thisGameObject = GameObjects[i];
                thisGameObject.Update();
            }
        }

        private void DrawLowerFloors()
        {
            int width = leftWalls.GetLength(0);
            int height = leftWalls.GetLength(1);
            int depth = leftWalls.GetLength(2);
            int maxDepth = Math.Min(ClientPlayer.Position.Z, depth);

            for (int z = 0; z < maxDepth; z++)
            {
                int zDiff = ClientPlayer.Position.Z - z;
                Render.OffsetPosition(new Vector2f(0f, zDiff * wallSpriteHeight));
                int x, y;
                //south end, top walls
                y = height - 1;
                for (x = 0; x < width; x++)
                {
                    Wall thisTopWall = topWalls[x, y, z];
                    if (thisTopWall != null)
                        thisTopWall.Draw(Layer.LowerWalls);
                }

                //east end, left walls
                x = width - 1;
                for (y = 0; y < height; y++)
                {
                    Wall thisLeftWall = leftWalls[x, y, z];
                    if (thisLeftWall != null)
                        thisLeftWall.Draw(Layer.LowerWalls);
                }
            }
            Render.OffsetPosition(new Vector2f(0f, 0f));
        }

        private void DrawTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    for (int z = 0; z < FLOORS; z++)
                    {
                        if (z == ClientPlayer.Position.Z)
                        {
                            Tile thisTile = tiles[x, y, z];
                            if (thisTile != null)
                                thisTile.Draw();
                        }
                    }
                }
            }
        }

        private void DrawWalls()
        {
            int width = leftWalls.GetLength(0);
            int height = leftWalls.GetLength(1);
            int depth = leftWalls.GetLength(2);
            int z = ClientPlayer.Position.Z;

            //z out of bounds
            if (z < 0 || z >= depth)
                return;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Wall thisLeftWall = leftWalls[x, y, z];
                    Wall thisTopWall = topWalls[x, y, z];
                    var layer = Helper.TilePosToLayer(new Vector2i(x, y));

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
            for (int i = 0; i < Actors.Count; i++)
            {
                Player thisPlayer = (Player)Actors[i];
                if (thisPlayer.Position.Z == ClientPlayer.Position.Z)
                {
                    //it's -1 because there's a corner case with topWall directly down, leftWall directly right
                    int thisRealY = thisPlayer.IsoPosition.Y - 1;
                    float lerpVal = thisRealY / (float)MaxRealY;
                    float layer = Helper.Lerp(Layer.WallFar, Layer.WallNear, lerpVal);
                    thisPlayer.Draw(layer);
                }
            }
        }

        private void DrawGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].Position.Z == ClientPlayer.Position.Z)
                {
                    GameObject thisGameObject = GameObjects[i];
                    thisGameObject.Draw();
                }
            }
        }

        /// <summary>
        /// Add a tile at an array index, not world coordinates
        /// </summary>
        public void AddTile(int x, int y, int z, int type)
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
        /// Get a wall at an array index, not world coordinates
        /// </summary>
        public void AddWall(int x, int y, int z, int type, bool leftFacing)
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

        /// <summary>
        /// Get a tile at an array index, not world coordinates
        /// </summary>
        public Tile GetTile(int x, int y, int z)
        {
            return tiles[x, y, z];
        }

        /// <summary>
        /// Get a top wall at an array index, not world coordinates
        /// </summary>
        public Wall GetWallTop(int x, int y, int z)
        {
            return topWalls[x, y, z];
        }

        /// <summary>
        /// Get a left wall at an array index, not world coordinates
        /// </summary>
        public Wall GetWallLeft(int x, int y, int z)
        {
            return leftWalls[x, y, z];
        }
    }
}
