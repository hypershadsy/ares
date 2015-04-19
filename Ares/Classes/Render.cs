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
        public static List<LayeredDrawable> spriteBatch = new List<LayeredDrawable>();

        public static void Draw(Texture texture, Vector2f position, Color color, Vector2f origin, int facing, float rotation, float layer = 0.0f)
        {
            DrawGenericTexture(texture, position, color, origin, facing, rotation, null, layer);
        }

        public static void DrawString(Font font, String message, Vector2f position, Color color, float scale, bool centered, float layer = 0.0f)
        {
            Text text = new Text(message, font);
            text.Scale = new Vector2f(scale, scale);
            text.Position = position;
            text.Color = color;
            if (centered)
                text.Position = new Vector2f(text.Position.X - ((text.GetLocalBounds().Width * scale) / 2), text.Position.Y);

            LayeredDrawable layeredText = new LayeredDrawable();
            layeredText.Layer = layer;
            layeredText.Drawable = text;
            spriteBatch.Add(layeredText);
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

            DrawGenericTexture(texture, position, color, origin, facing, 0f, source, layer);
        }

        //TODO: fix facing origin (-1 doesn't reflect about its center)
        private static void DrawGenericTexture(Texture texture, Vector2f position, Color color, Vector2f origin, int facing, float rotation, IntRect? textureRect, float layer)
        {
            Sprite sprite = new Sprite(texture);
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

            LayeredDrawable layeredSprite = new LayeredDrawable();
            layeredSprite.Drawable = sprite;
            layeredSprite.Layer = layer;
            spriteBatch.Add(layeredSprite);
        }

        public static void SpitToWindow()
        {
            spriteBatch.OrderBy(drawable => drawable.Layer); //stable sort, 0 near, 1 far

            //TODO: if we don't care about the depth, skip the list and draw anyway
            foreach (LayeredDrawable layered in spriteBatch)
            {
                Drawable drawable = layered.Drawable;
                Game.window.Draw(drawable);
            }

            spriteBatch.Clear();
        }
    }
}
