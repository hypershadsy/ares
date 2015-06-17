using SFML.Graphics;
using System;
using System.IO;
using System.Collections.Generic;

namespace Ares
{
    class Content
    {
        private static Dictionary<string, Texture> textures;
        private static Dictionary<string, Font> fonts;

        public static string RootDirectory { get; set; }

        static Content()
        {
            RootDirectory = "Content";
            textures = new Dictionary<string, Texture>();
            fonts = new Dictionary<string, Font>();
        }

        public static Texture GetTexture(string what)
        {
            PrecacheTexture(what);
            return textures[what];
        }

        public static Font GetFont(string what)
        {
            PrecacheFont(what);
            return fonts[what];
        }

        public static void PrecacheTexture(string what)
        {
            if (textures.ContainsKey(what)) { return; }
            string thePath = Path.Combine(RootDirectory, what);
            Texture theTex = new Texture(thePath);
            textures.Add(what, theTex);
        }

        public static void PrecacheFont(string what)
        {
            if (fonts.ContainsKey(what)) { return; }
            string thePath = Path.Combine(RootDirectory, what);
            Font theFont = new Font(thePath);
            fonts.Add(what, theFont);
        }
    }
}
