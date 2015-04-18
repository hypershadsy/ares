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
    public static class Render
    {
        public static List<LayeredSprite> spriteBatch = new List<LayeredSprite>();

        //TODO: fix facing origin (-1 doesn't reflect about its center)
        public static void Draw(Texture texture, Vector2f position, Color color, Vector2f origin, int facing, float rotation, float depth = 0.0f)
        {
            LayeredSprite sprite = new LayeredSprite(texture);
            sprite.Texture.Smooth = false;
            sprite.Scale = new Vector2f(facing, 1);
            sprite.Origin = origin;
            sprite.Position = position;
            sprite.Color = color;
            sprite.Rotation = rotation;
            sprite.Layer = depth;
            //TODO: if we don't care about the depth, skip the list and draw anyway
            spriteBatch.Add(sprite);
        }

        public static void DrawString(Font font, String message, Vector2f position, Color color, float scale, bool centered)
        {
            Text text = new Text(message, font);
            text.Scale = new Vector2f(scale, scale);
            text.Position = position;
            text.Color = color;
            if (centered)
                text.Position = new Vector2f(text.Position.X - ((text.GetLocalBounds().Width * scale) / 2), text.Position.Y);
            Game.window.Draw(text);
        }

        public static void DrawAnimation(Texture texture, Vector2f position, Color color, Vector2f origin, int facing, int frames, int rows, ref int currentFrame, int frameRow)
        {
            Sprite sprite = new Sprite(texture);
            sprite.Scale = new Vector2f(facing, 1);
            sprite.Origin = origin;
            sprite.Position = position;
            sprite.Color = color;
            sprite.TextureRect = RunAnimation(sprite, (int)(sprite.Texture.Size.X / frames), (int)(sprite.Texture.Size.Y), ref currentFrame, frameRow, rows);
            Game.window.Draw(sprite);
        }

        private static IntRect RunAnimation(Sprite texture, int widthOfFrame, int heightOfFrame, ref int CurrFrame, int currentRow, int totalRows)
        {
            IntRect source = new IntRect(0, 0, 0, 0);

            if (CurrFrame >= (texture.Texture.Size.X / widthOfFrame))
                CurrFrame = 0;

            source.Left = (CurrFrame * widthOfFrame);
            source.Width = widthOfFrame;
            heightOfFrame = (int)texture.Texture.Size.Y / totalRows;
            source.Top = (int)((heightOfFrame) * (currentRow - 1));
            source.Height = heightOfFrame;

            return source;
        }

        public static void SpitToWindow()
        {
            spriteBatch.Sort((x, y) => {
                return x.Layer.CompareTo(y.Layer);
            });

            foreach (LayeredSprite sprite in spriteBatch)
            {
                Game.window.Draw(sprite);
            }

            spriteBatch.Clear();
        }
    }
}
