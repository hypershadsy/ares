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

        public static void Draw(Texture texture, Vector2f position, Color color, Vector2f origin, int facing, float rotation, float layer = 0.0f)
        {
            DrawGeneric(texture, position, color, origin, facing, rotation, null, layer);
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

        public static void DrawAnimation(Texture texture, Vector2f position, Color color, Vector2f origin, int facing, int totalFrames, int totalRows, int currentFrame, int currentRow, float layer = 0.0f)
        {
            int widthOfFrame = (int)(texture.Size.X / totalFrames);
            int heightOfFrame = (int)(texture.Size.Y / totalRows);

            IntRect source = new IntRect(
                widthOfFrame * currentFrame,
                heightOfFrame * (currentRow - 1),
                widthOfFrame,
                heightOfFrame
            );

            DrawGeneric(texture, position, color, origin, facing, 0f, source, layer);
        }

        //TODO: fix facing origin (-1 doesn't reflect about its center)
        private static void DrawGeneric(Texture texture, Vector2f position, Color color, Vector2f origin, int facing, float rotation, IntRect? textureRect, float layer)
        {
            LayeredSprite sprite = new LayeredSprite(texture);
            sprite.Texture.Smooth = false;
            sprite.Scale = new Vector2f(facing, 1);
            sprite.Origin = origin;
            sprite.Position = position;
            sprite.Color = color;
            sprite.Rotation = rotation;
            if (textureRect.HasValue)
            {
                sprite.TextureRect = textureRect.Value;
            }
            sprite.Layer = layer;
            //TODO: if we don't care about the depth, skip the list and draw anyway
            spriteBatch.Add(sprite);
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
