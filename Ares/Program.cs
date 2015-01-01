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

        public static NetClient client;

        public static View camera2D;

        public static Font font;
        public static Sprite charSprite;


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
            window = new RenderWindow(
                new VideoMode(800, 600), "Project Ares");

            windowSize = new Vector2f(800, 600);
            window.SetFramerateLimit(60);

            window.Closed += (a, b) =>
                {
                    window.Close();
                };

            camera2D = new View(new Vector2f(640 / 2, 480 / 2), new Vector2f(640, 480));



            charSprite = new Sprite(new Texture("Content/"));
            font = new Font("Content/Font1.ttf");

        }


        private static void UpdateDraw(RenderWindow window)
        {
            window.DispatchEvents();
            window.Clear(Color.Green);
            Input.Update();

            

            window.Display();

            NetOutgoingMessage outgoing = client.CreateMessage();
            outgoing.Write("POS");
            outgoing.Write(100);
            outgoing.Write(150);

            client.SendMessage(outgoing, NetDeliveryMethod.UnreliableSequenced);

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
                        //read the incoming string
                        string messageType = msg.ReadString();


                        switch (messageType)
                        {
                            case "LIFE":
                                break;

                            case "NAME":
                                break;

                            case "POS": //Update a player's position

                                break;

                            case "JOIN": //Add a player
                                break;

                            case "CHAT": //Add chat
                                break;

                            case "PART": //Remove a player
                                break;
                        }

                        break;
                    default:
                        Console.WriteLine("Unrecognized Message Recieved:" + msg.ToString());
                        break;
                }
                Game.client.Recycle(msg);
            }

        }
    }
}



