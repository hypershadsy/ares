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
    public static class Input
    {
        public static List<string> PressedKeys = new List<string>() { };
        public static List<string> OldPressedKeys = new List<string>() { };
        public static bool isActive = true;

        public static void Update()
        {
            refreshState();
            PressedKeys.Clear();


            for (int i = 0; i < 100; i++)
            {
                if (Keyboard.IsKeyPressed((Keyboard.Key)i))
                {
                    PressedKeys.Add(((Keyboard.Key)i).ToString());

                }
            }
        }

        public static void refreshState()
        {
            OldPressedKeys = new List<string>(PressedKeys);
        }

        public static bool isKeyTap(Keyboard.Key key)
        {
            if (!isActive)
                return false;
            if (PressedKeys.Contains(key.ToString()) &&
                !OldPressedKeys.Contains(key.ToString()))
                return true;
            return false;
        }

        public static bool isKeyDown(Keyboard.Key key)
        {
            if (!isActive)
                return false;
            return Keyboard.IsKeyPressed(key);
        }

        public static bool isKeyUp(Keyboard.Key key)
        {
            if (!isActive)
                return false;
            return !Keyboard.IsKeyPressed(key);
        }
    }
}
