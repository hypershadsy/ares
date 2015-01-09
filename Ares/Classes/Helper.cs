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

        public static float AngleBetween(Vector2f vec1, Vector2f vec2)
        {
            return (float)Math.PI / 2 + (float)(Math.PI) - (float)Math.Atan2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public static Vector2f GetWorldMousePosition()
        {
            Vector2i pixel_pos = Mouse.GetPosition(Game.window);
            Vector2f coord_pos = Game.window.MapPixelToCoords(pixel_pos);

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
    }
}
