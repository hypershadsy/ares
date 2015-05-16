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
        public static DateTime startTime;
        public static Vector2f windowSize;
        public static Random r = new Random();
        public static DateTime oldDateTime;

        public static InternalGame internalGame;
        public static GameState gameState; //The holy and righteous GameState

        public static TimeSpan deltaTime
        {
            get
            {
                return DateTime.Now - oldDateTime;
            }
        }

        public static NetClient client;
        public static View camera2D;
        public static Font font;
        public static Texture charTexture, wallTexture, grassTexture, walktest, woodfloor,
            doorClosedTexture, pathTexture, bulletTexture, isoBlock, tileBedug, idletest,
            brickWallTexture, woodDoor1, tableBrown, cityBackground;

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
            //pretend value to appease the delta timer gods
            oldDateTime = DateTime.Now - new TimeSpan((long)expectedTicks);
            r = new Random();
            internalGame = new InternalGame();
            gameState = internalGame;
        }

        static void window_LostFocus(object sender, EventArgs e)
        {
            Input.isActive = false;
        }

        static void window_GainedFocus(object sender, EventArgs e)
        {
            Input.isActive = true;
        }

        private static void LoadContentInitialize()
        {
            //Load
            window = new RenderWindow(
                new VideoMode(800, 600), "Project Ares",Styles.Titlebar);

            windowSize = new Vector2f(800, 600);
            window.SetFramerateLimit(60);

            window.Closed += (a, b) =>
            {
                client.Disconnect("Bye");
                window.Close();
            };

            Game.window.GainedFocus += new EventHandler(window_GainedFocus);
            { }
            Game.window.LostFocus += new EventHandler(window_LostFocus);
            { }

            camera2D = new View(window.DefaultView);

            camera2D.Zoom(0.5f);

            charTexture = new Texture("Content/player.png");
            wallTexture = new Texture("Content/wall.png");
            grassTexture = new Texture("Content/grass.png");
            doorClosedTexture = new Texture("Content/doorClosed.png");
            pathTexture = new Texture("Content/path.png");
            bulletTexture = new Texture("Content/bullet.png");
            isoBlock = new Texture("Content/isoBlock.png");
            font = new Font("Content/Font1.ttf");
            idletest = new Texture("Content/debug/idletest.png");
            walktest = new Texture("Content/debug/walktest.png");
            tileBedug = new Texture("Content/tile/bedug.png");
            woodfloor = new Texture("Content/tile/woodfloor.png");
            brickWallTexture = new Texture("Content/wall/redbrick.png");
            woodDoor1 = new Texture("Content/wall/door/door2.png");
            tableBrown = new Texture("Content/furniture/tableBrown.png");
            cityBackground = new Texture("Content/debug/background.png");
            //Initialize
            NetPeerConfiguration config = new NetPeerConfiguration("ares");
            config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            string ip = "giga.krash.net"; //Jared's IP
            int port = 12345;
            client = new NetClient(config);
            client.Start();

            internalGame.map = new Map(20);

            //start processing messages
            client.Connect(ip, port);
        }

        private static void UpdateDraw(RenderWindow window)
        {
            window.Clear(Color.Black);
            HandleMessages();
            window.DispatchEvents();
            Input.Update();
            gameState.Update();
            gameState.Draw();
            window.Display();
            oldDateTime = DateTime.Now;

            if (Input.isKeyDown(Keyboard.Key.Escape))
            {
                window.Close();
            }
        }

        public static void HandleMessages()
        {
            NetIncomingMessage msg;
            while ((msg = Game.client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                    case NetIncomingMessageType.Data:
                        //multiple game messages in a single packet
                        if (msg.PeekString() == "MULTI_ON")
                        {
                            //consume start marker
                            msg.ReadString();
                            //read until end marker is reached
                            while (msg.PeekString() != "MULTI_OFF")
                            {
                                HandleAGameMessage(msg);
                            }
                        }
                        else //regular single message
                        {
                            HandleAGameMessage(msg);
                        }
                        break;
                    default:
                        Console.WriteLine("Unrecognized Lidgren Message Recieved: {0}", msg.MessageType);
                        break;
                }
                Game.client.Recycle(msg);
            }
        }

        private static void HandleAGameMessage(NetIncomingMessage msg)
        {
            string messageType = msg.ReadString(); //Unsafe code

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
                    var UID_POS = msg.ReadInt64();
                    var xPos = msg.ReadInt32();
                    var yPos = msg.ReadInt32();
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

                case "TILE": //Recieves a tile of type 'tileType'
                    var xTilePos = msg.ReadInt32();
                    var yTilePos = msg.ReadInt32();
                    var tileType = msg.ReadInt32();
                    handleTileMessage(new Vector2i(xTilePos, yTilePos), tileType);
                    break;

                case "WALL": //Recieves a wall of type 'wallType'
                    var xWallPos = msg.ReadInt32();
                    var yWallPos = msg.ReadInt32();
                    var wallType = msg.ReadInt32();
                    bool leftFacing = msg.ReadBoolean();
                    handleWallMessage(new Vector2i(xWallPos, yWallPos), wallType, leftFacing);
                    break;

                case "OBJ_CREATE":
                    int objUID = msg.ReadInt32();
                    int objType = msg.ReadInt32();

                    switch (objType)
                    {
                        case 0: //Basic Brown Table
                            int objX = msg.ReadInt32();
                            int objY = msg.ReadInt32();
                            bool objLeftFacing = msg.ReadBoolean();

                            internalGame.map.GameObjects.Add(new BasicBrownTable(new Vector2i(objX, objY),objUID, objLeftFacing));
                            break;
                    }
                    break;

                case "INFO": //Recieved when server has completed sending all newbie initialization
                    break;
                
                default:
                    Console.WriteLine("Unrecognized Game Message Recieved: {0}\n{1}", msg.ToString(), messageType);
                    break;
            }
        }

        private static void handleLifeMessage(long uid, int health)
        {
            getPlayerWithUID(uid).Health = health;
        }

        private static void handleNameMessage(long uid, string newName)
        {
            getPlayerWithUID(uid).Name = newName;
        }

        private static void handlePosMessage(long uid, int x, int y)
        {
            Actor plr = getPlayerWithUID(uid);
            if (plr != null) //stale POS message, player is already gone?
            {
                plr.Position = new Vector2i(x, y);
            }
        }

        private static void handleJoinMessage(long uid)
        {
            //add a new net player to players
            internalGame.map.Actors.Add(new NetPlayer(uid));
        }

        private static void handleChatMessage(long uid, string message)
        {
            Player p = getPlayerWithUID(uid);
            internalGame.map.ClientPlayer.gui.chat.messages.Add(
                new ChatMessage(message, p));
        }

        private static void handlePartMessage(long uid)
        {
            //remove net player from players list
            Game.internalGame.map.Actors.Remove(getPlayerWithUID(uid));
        }

        private static Player getPlayerWithUID(long id)
        {
            for (int i = 0; i < internalGame.map.Actors.Count; i++)
            {
                if (internalGame.map.Actors[i] is Player && ((Player)internalGame.map.Actors[i]).UID == id)
                    return (Player)internalGame.map.Actors[i];
            }

            return null;
        }

        private static void handleTileMessage(Vector2i pos, int type)
        {
            internalGame.map.addTile(pos.X, pos.Y, type);
        }

        private static void handleWallMessage(Vector2i pos, int type, bool leftFacing)
        {
            internalGame.map.addWall(pos.X, pos.Y, type, leftFacing);
        }


        /// <summary>
        /// Gets the delta ratio. If the game is running slowly, this number will be higher,
        /// causing your game object to go further per frame. At 60FPS, this number will be 1.0.
        /// To take care of flukes in frametime, the ratio is averaged out over a period of
        /// <c>deltaPeriod</c> frames.
        /// </summary>
        /// <returns>The delta ratio.</returns>
        static Queue<double> pastFrameTimes;
        const double expectedTicks = (1000.0 / 63.0) * 10000.0;
        const int deltaPeriod = 100;

        public static float getDeltaRatio()
        {
            double actualTicks = deltaTime.Ticks;
            double ratio = actualTicks / expectedTicks;
            //debugging screws up timestep, we'll assume it's running fine
            if (double.IsInfinity(ratio) || double.IsNaN(ratio))
            {
                ratio = 1.0;
            }

            if (pastFrameTimes == null)
            {
                pastFrameTimes = new Queue<double>(Enumerable.Repeat(1.0, deltaPeriod).ToList());
                ratio = 1.0; //initialization is tough... ignore the first frame
            }

            //prune the old ratio, add this one
            pastFrameTimes.Dequeue();
            pastFrameTimes.Enqueue(ratio);

            var avg = pastFrameTimes.Average();

            return (float)avg;
        }
    }
}



