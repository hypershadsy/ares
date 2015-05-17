using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace Ares
{
    public class InternalGame : GameState
    {
        private List<Map> floors = new List<Map>() { };
        public int currentFloor = 1;
        public ClientPlayer ClientPlayer;

        public List<Actor> Actors = new List<Actor>();
        public List<GameObject> GameObjects = new List<GameObject>();

        public InternalGame()
            : base()
        {
            createFloors(20, 3);
            ClientPlayer = new ClientPlayer(floors[1], new Vector2i(1, 1));

            Actors.Add(ClientPlayer);
        }

        public override void Update()
        {
            base.Update();
            updateAllFloors();
            UpdatePlayers();
            UpdateGameObjects();
        }

        public override void Draw()
        {
            base.Draw();

            Game.window.SetView(Game.camera2D);
            drawCurrentFloor();
            DrawPlayers();
            DrawGameObjects();
            Render.Draw(Game.cityBackground, new Vector2f(0, 0), Color.White, new Vector2f(1066, 818), 1, 0f, 1);
            Render.SpitToWindow();

            Game.window.SetView(Game.window.DefaultView);
            ClientPlayer.gui.Draw();
            Render.SpitToWindow();
        }

        public List<Map> getFloors()
        {
            return floors;
        }

        public void createFloors(int size, int numFloors)
        {
            for (int i = 0; i < numFloors; i++)
            {
                floors.Add(new Map(size, i));
            }
        }

        public Map getCurrentFloor()
        {
            return floors[currentFloor];
        }

        private void updateAllFloors()
        {
            for (int i = 0; i < floors.Count; i++)
            {
                floors[i].Update();
            }
        }
        private void drawCurrentFloor()
        {
            floors[currentFloor].Draw();
        }

        private void UpdatePlayers()
        {
            for (int i = 0; i < Actors.Count; i++)
            {
                Player thisPlayer = (Player)Actors[i];
                thisPlayer.Update();
            }
        }

        private void DrawPlayers()
        {
            for (int i = 0; i < Actors.Count; i++)
            {
                Player thisPlayer = (Player)Actors[i];
                //it's -1 because there's a corner case with topWall directly down, leftWall directly right
                int thisRealY = thisPlayer.IsoPosition.Y - 1;
                float lerpVal = thisRealY / (float)floors[currentFloor].MaxRealY;
                float layer = Helper.Lerp(Layer.WallFar, Layer.WallNear, lerpVal);
                thisPlayer.Draw(layer);
            }
        }

        public void DrawGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject thisGameObject = GameObjects[i];
                thisGameObject.Draw();
            }
        }

        public void UpdateGameObjects()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject thisGameObject = GameObjects[i];
                thisGameObject.Update();
            }
        }

    }
}
