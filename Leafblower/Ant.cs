using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Ant : Entity
    {
        private Texture Texture;
        private IntRect[] AnimationFrames;
        private int Frame;
        private int FrameTime;
        private float Angle;
        private float WalkingSpeed;
        private Vector2f Speed;
        private byte Alpha;

        public Ant(float x, float y)
        {
            Texture = Resources.Textures["ant"];
            AnimationFrames = new IntRect[] { new IntRect(0, 0, 43, 39), new IntRect(43, 0, 43, 39) };
            Frame = Game.Random.Next(2);
            FrameTime = Game.Random.Next(10);
            Sprite = new Sprite(Texture, AnimationFrames[Frame]);
            HitPoint = new Vector2f(x, y);
            HitRadius = 15;
            Angle = (float)Game.Random.NextDouble() * 360;
            WalkingSpeed = 1;
            Sprite.Origin = new Vector2f(22, 20);
            Sprite.Position = HitPoint;
            Sprite.Rotation = Angle;
            Alpha = 255;
        }

        public override void Update(Level level)
        {
            if (!Collected)
            {
                bool b = level.Leafblower.Blowing && level.Leafblower.InBlowingRange(HitPoint.X, HitPoint.Y);
                if (b)
                {
                    Speed = level.Leafblower.GetSpeedVector(HitPoint.X, HitPoint.Y);
                }
                else
                {
                    Speed *= 0.9f;
                }
                HitPoint += Speed * 800;
                double a = Angle * Math.PI / 180;
                HitPoint += new Vector2f((float)Math.Cos(a) * WalkingSpeed, (float)Math.Sin(a) * WalkingSpeed);
                Sprite.Position = HitPoint;
                float dX = HitPoint.X - level.Center.X;
                float dY = HitPoint.Y - level.Center.Y;
                if (dX * dX + dY * dY < 6400)
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
                if (FrameTime > 10)
                {
                    Frame++;
                    if (Frame > 1)
                    {
                        Frame = 0;
                    }
                    Sprite.TextureRect = AnimationFrames[Frame];
                    FrameTime = 0;
                }
            }
            else
            {
                if (Alpha > 0)
                {
                    Alpha -= 8;
                    if (Alpha < 8)
                    {
                        Alpha = 0;
                    }
                    Sprite.Color = new Color(255, 255, 255, Alpha);
                }
            }
        }

        public override void Draw()
        {
            if (Alpha > 0)
            {
                Game.Window.Draw(Sprite);
            }
        }
    }
}
