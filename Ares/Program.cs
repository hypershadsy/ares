using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using Lidgren.Network;


namespace SfmlTestProj
{
    class Program
    {
        public static RenderWindow window;
        static DateTime startTime;
        static Vector2f windowSize;
        static Random r = new Random();

        public static View camera2D;

        public static Font font;

        static void Main(string[] args)
        {
            PreRun();
            LoadContentInitialize();

            while (window.IsOpen())
            {
                UpdateDraw(window);
            }
        }

        private static void PreRun()
        {
            startTime = DateTime.Now;
            r = new Random(100);
        }

        private static void LoadContentInitialize()
        {
            window = new RenderWindow(
                new VideoMode(800,600), "Project Title");

            windowSize = new Vector2f(800,600);
            window.SetFramerateLimit(60);

            window.Closed += (a, b) => 
                {
                    window.Close();
                };

            camera2D = new View(new Vector2f(640/2,480/2), new Vector2f(640,480));




            font = new Font("Content/Font1.ttf");

        }


        private static void UpdateDraw(RenderWindow window)
        {
            window.DispatchEvents();
            window.Clear(Color.Green);

            

            window.Display();

          
        }
    }
}



