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
    public static class Helper
    {
        public static float Distance(Vector2f vec1, Vector2f vec2)
        {
            return (float)Math.Sqrt(Math.Pow((vec2.X - vec1.X), 2) + Math.Pow((vec2.Y - vec1.Y), 2));
        }
        public static float Distance(Vector2i vec1, Vector2i vec2)
        {
            return (float)Math.Sqrt(Math.Pow((vec2.X - vec1.X), 2) + Math.Pow((vec2.Y - vec1.Y), 2));
        }

        public static float AngleBetween(Vector2f vec1, Vector2f vec2)
        {
            return (float)Math.PI / 2 + (float)(Math.PI) - (float)Math.Atan2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public static Vector2f GetWorldMousePosition()
        {
            Vector2i pixel_pos = Mouse.GetPosition(Game.window);
            Vector2f coord_pos = Game.window.MapPixelToCoords(pixel_pos);
            //TODO: isometric

            return coord_pos;
        }

        public static float DegToRad(float degree)
        {
            return degree * ((float)Math.PI / 180f);
        }

        public static float RadToDeg(float rad)
        {
            return rad * (180f / (float)Math.PI);
        }

        /// <summary>
        /// Converts tilespace to isospace.
        /// Given a position expressed as tile coordinates (e.g. 3,5), get the isometric position at the top
        /// left hand corner of the tile.
        /// </summary>
        public static Vector2i TileToIso(Vector2i tileSpace)
        {
            int realX = 0;
            int realY = 0;
            realX += tileSpace.X * 32;
            realX -= tileSpace.Y * 32;
            realY += tileSpace.Y * 16;
            realY += tileSpace.X * 16;
            return new Vector2i(realX, realY);
        }

        public static Vector2i IsoToTile(Vector2i isoSpace) //This may be broken as fuck
        {
            int realX = 0;
            int realY = 0;
            realX -= isoSpace.X / 32;
            realX += isoSpace.Y / 32;
            realY -= isoSpace.Y / 16;
            realY -= isoSpace.X / 16;
            return new Vector2i(realX, realY);
        }

        public static float TilePosToLayer(Vector2i input)
        {
            int thisRealY = Helper.TileToIso(input).Y;
            float lerpVal = thisRealY / (float)(Game.internalGame.getCurrentFloor().MaxRealY);
            float layer = Helper.Lerp(Layer.WallFar, Layer.WallNear, lerpVal);
            return layer;
        }

        public static void moveCameraTo(View camera, Vector2f focus, float speed)
        {
            if (camera.Center.X > focus.X)
            {
                camera.Center -= new Vector2f((Math.Abs(camera.Center.X - focus.X) * speed), 0);
            }

            if (camera.Center.X < focus.X)
            {
                camera.Center += new Vector2f((Math.Abs(camera.Center.X - focus.X) * speed), 0);
            }

            if (camera.Center.Y > focus.Y)
            {
                camera.Center -= new Vector2f(0, (Math.Abs(camera.Center.Y - focus.Y) * speed));
            }

            if (camera.Center.Y < focus.Y)
            {
                camera.Center += new Vector2f(0, Math.Abs(camera.Center.Y - focus.Y) * speed);
            }
        }

        public static float Lerp(float v0, float v1, float t)
        {
            return (1 - t) * v0 + t * v1;
        }
    }
}
