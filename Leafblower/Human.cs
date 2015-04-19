using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Human : Entity
    {
        private Texture Texture;
        private IntRect[] AnimationFrames;
        private int Frame;
        private int FrameTime;
        private float Angle;
        private float WalkingSpeed;
        private Vector2f Speed;

        public Human(float x, float y)
        {
            Texture = Resources.Textures["human"];
            AnimationFrames = new IntRect[] { new IntRect(0, 0, 39, 29), new IntRect(39, 0, 39, 29), new IntRect(78, 0, 39, 29), new IntRect(117, 0, 39, 29) };
            Frame = Game.Random.Next(4);
            FrameTime = Game.Random.Next(10);
            Sprite = new Sprite(Texture, AnimationFrames[Frame]);
            HitPoint = new Vector2f(x, y);
            HitRadius = 15;
            Angle = (float)Game.Random.NextDouble() * 360;
            WalkingSpeed = 2;
            Sprite.Origin = new Vector2f(16, 13);
            Sprite.Position = HitPoint;
            Sprite.Rotation = Angle;
        }

        public static bool CanGo(float x, float y)
        {
            return (x > 220 && x < 280) || (y > 190 && y < 270);
        }

        public override void Update(Level level)
        {
            if (!Collected)
            {
                bool b = level.Leafblower.Blowing && level.Leafblower.InBlowingRange(HitPoint.X, HitPoint.Y);
                if (b)
                {
                    Speed = level.Leafblower.GetSpeedVector(HitPoint.X, HitPoint.Y);
                    WalkingSpeed = 4;
                }
                else
                {
                    Speed *= 0.9f;
                    WalkingSpeed = 2;
                }
                //HitPoint += Speed * 600;
                double a = Angle * Math.PI / 180;
                //HitPoint += new Vector2f((float)Math.Cos(a) * WalkingSpeed, (float)Math.Sin(a) * WalkingSpeed);
                float x = HitPoint.X + Speed.X * 500 + (float)Math.Cos(a) * WalkingSpeed;
                float y = HitPoint.Y + Speed.Y * 500 + (float)Math.Sin(a) * WalkingSpeed;
                bool t = false;
                if (CanGo(x, HitPoint.Y))
                {
                    HitPoint.X = x;
                }
                else
                {
                    t = true;
                }
                if (CanGo(HitPoint.X, y))
                {
                    HitPoint.Y = y;
                }
                else
                {
                    t = true;
                }
                if (t && !b)
                {
                    Angle += 180;
                    Sprite.Rotation = Angle;
                }
                Sprite.Position = HitPoint;
                float dX = HitPoint.X - level.Center.X;
                float dY = HitPoint.Y - level.Center.Y;
                if (dX * dX + dY * dY < 10000)
                {
                    if (dX > 0)
                    {
                        Angle = (float)(Math.Atan(dY / dX) / Math.PI * 180);
                    }
                    else
                    {
                        Angle = (float)(Math.Atan(dY / dX) / Math.PI * 180) + 180;
                    }
                    Sprite.Rotation = Angle;
                }
                if (HitPoint.X < HitRadius)
                {
                    HitPoint = new Vector2f(HitRadius, HitPoint.Y);
                    if (!b)
                    {
                        Angle = 180 - Angle;
                        Sprite.Rotation = Angle;
                    }
                }
                else if (HitPoint.X > Game.Width - HitRadius)
                {
                    HitPoint = new Vector2f(Game.Width - HitRadius, HitPoint.Y);
                    if (!b)
                    {
                        Angle = 180 - Angle;
                        Sprite.Rotation = Angle;
                    }
                }
                if (HitPoint.Y < HitRadius)
                {
                    HitPoint = new Vector2f(HitPoint.X, HitRadius);
                    if (!b)
                    {
                        Angle = -Angle;
                        Sprite.Rotation = Angle;
                    }
                }
                else if (HitPoint.Y > Game.Height - HitRadius)
                {
                    HitPoint = new Vector2f(HitPoint.X, Game.Height - HitRadius);
                    if (!b)
                    {
                        Angle = -Angle;
                        Sprite.Rotation = Angle;
                    }
                }
                FrameTime++;
                int f;
                if (!b)
                {
                    f = 10;
                }
                else
                {
                    f = 6;
                }
                if (FrameTime > f)
                {
                    Frame++;
                    if (Frame > 3)
                    {
                        Frame = 0;
                    }
                    Sprite.TextureRect = AnimationFrames[Frame];
                    FrameTime = 0;
                }
            }
        }

        public override void Draw()
        {
            if (!Collected)
            {
                Game.Window.Draw(Sprite);
            }
        }
    }
}
