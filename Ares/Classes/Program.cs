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
    class Game
    {
        public static RenderWindow window;
        static DateTime startTime;
        static Vector2f windowSize;
        static Random r = new Random();

        static DateTime oldDateTime;
        static TimeSpan deltaTime;


        public static NetClient client;

        public static View camera2D;

        public static Font font;
        public static Texture charTexture, wallTexture;

        public static Map map;


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
            r = new Random();
            Initialize();
        }

        private static void Initialize()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("ares");
            config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            string ip = "giga.krash.net"; //Jared's IP
            int port = 12345;
            client = new NetClient(config);
            client.Start();
            client.Connect(ip, port);




        }

        private static void LoadContentInitialize()
        {
            //Load
            window = new RenderWindow(
                new VideoMode(800, 600), "Project Ares");

            windowSize = new Vector2f(800, 600);
            window.SetFramerateLimit(60);

            window.Closed += (a, b) =>
                {
                    client.Disconnect("Bye");
                    window.Close();
                };

            camera2D = new View(new Vector2f(800 / 2, 600 / 2), new Vector2f(0, 0));

            charTexture = new Texture("Content/player.png");
            wallTexture = new Texture("Content/wall.png");
            font = new Font("Content/Font1.ttf");


            //Initialize
            map = new Map(20);
        }

        private static void UpdateDraw(RenderWindow window)
        {
            deltaTime = DateTime.Now - oldDateTime;
            HandleMessages();
            window.DispatchEvents();
            window.Clear(Color.Green);



            Input.Update();

            map.Update();
            map.Draw();




            oldDateTime = DateTime.Now;
            window.Display();
        }

        public static void HandleMessages()
        {
            NetIncomingMessage msg;
            while ((msg = Game.client.ReadMessage()) != null)
            {

                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                    case NetIncomingMessageType.Data:
                        //while (msg.PeekString() != string.Empty)
                        //{
                        //read the incoming string
                        string messageType = msg.ReadString();

                        switch (messageType)
                        {
                            case "LIFE":
                                long UID_LIFE = msg.ReadInt64();
                                int hp = msg.ReadInt32();
                                handleLifeMessage(UID_LIFE, hp);
                                break;

                            case "NAME":
                                long UID_NAME = msg.ReadInt64();
                                string newName = msg.ReadString();
                                handleNameMessage(UID_NAME, newName);
                                break;

                            case "POS": //Update a player's position
                                long UID_POS = msg.ReadInt64();
                                float xPos = msg.ReadFloat();
                                float yPos = msg.ReadFloat();
                                handlePosMessage(UID_POS, xPos, yPos);
                                break;

                            case "JOIN": //Add a player
                                long UID_JOIN = msg.ReadInt64();
                                handleJoinMessage(UID_JOIN);
                                break;

                            case "CHAT": //Add chat
                                long UID_CHAT = msg.ReadInt64();
                                string message = msg.ReadString();
                                handleChatMessage(UID_CHAT, message);
                                break;

                            case "PART": //Remove a player
                                long UID_PART = msg.ReadInt64();
                                handlePartMessage(UID_PART);
                                break;
                        }
                        //}
                        break;
                    default:
                        Console.WriteLine("Unrecognized Message Recieved:" + msg.ToString());
                        break;
                }
                Game.client.Recycle(msg);
            }

        }

        private static void handleLifeMessage(long uid, int health)
        {
            getPlayerWithUID(uid).health = health;
        }
        private static void handleNameMessage(long uid, string newName)
        {
            getPlayerWithUID(uid).name = newName;
        }
        private static void handlePosMessage(long uid, float x, float y)
        {
            getPlayerWithUID(uid).Position = new Vector2f(x, y);
        }
        private static void handleJoinMessage(long uid)
        {
            map.players.Add(new NetPlayer(uid));
            //add a new net player to players 
        }
        private static void handleChatMessage(long uid, string message)
        {
        }
        private static void handlePartMessage(long uid)
        {
            //remove net player from players list
        }


        private static Player getPlayerWithUID(long id)
        {
            for (int i = 0; i < map.players.Count; i++)
            {
                if (map.players[i].UID == id)
                    return map.players[i];
            }

            return null;
        }
        /// <summary>
        /// Gets the delta ratio. If the game is running slowly, this number will be higher,
        /// causing your game object to go further per frame. At 60FPS, this number will be 1.0.
        /// </summary>
        /// <returns>The delta ratio.</returns>
        public static float getDeltaRatio()
        {
            double sixtyFpsHundredNanos = 166666.66666666667;
            double actualHundredNanos = deltaTime.Ticks;
            double ratio = sixtyFpsHundredNanos / actualHundredNanos;
            //debugging screws up timestep, we'll assume it's running fine
            if (double.IsInfinity(ratio) || double.IsNaN(ratio))
            {
                ratio = 1.0;
            }
            return (float)ratio;
        }
    }
}



