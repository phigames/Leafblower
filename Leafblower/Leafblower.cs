using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace Leafblower
{
    class Leafblower : Entity
    {
        private Vector2f Center;
        private float Angle;
        private List<LeafblowerAir> Airs;
        private int AirTime;
        public bool Blowing;
        private bool TogglingBlow;
        private Sound BlowSound;

        public Leafblower(Vector2f center)
        {
            Center = center;
            Sprite = new Sprite(Resources.Textures["leafblower"]);
            Sprite.Origin = new Vector2f(16, 10);
            Sprite.Position = Center;
            Angle = 0;
            Airs = new List<LeafblowerAir>();
            BlowSound = Resources.Sounds["blow"];
        }

        public bool InBlowingRange(float x, float y)
        {
            double a = Math.Abs((Math.Atan((y - Sprite.Position.Y) / (x - Sprite.Position.X)) - Math.PI / 2) / Math.PI * 180 - Angle);
            if (a > 90)
            {
                a = 180 - a;
            }
            if (Math.Abs(a) > 30)
            {
                return false;
            }
            float m = Math.Abs((Sprite.Position.Y - Center.Y) / (Sprite.Position.X - Center.X));
            if (m > 1)
            {
                if (Sprite.Position.Y > Center.Y)
                {
                    return y < Sprite.Position.Y;
                }
                else
                {
                    return y > Sprite.Position.Y;
                }
            }
            else
            {
                if (Sprite.Position.X > Center.X)
                {
                    return x < Sprite.Position.X;
                }
                else
                {
                    return x > Sprite.Position.X;
                }
            }
        }

        public Vector2f GetSpeedVector(float x, float y)
        {
            float dX = x - Sprite.Position.X;
            float dY = y - Sprite.Position.Y;
            float d = dX * dX + dY * dY + 1;
            return new Vector2f(dX / d, dY / d);
        }

        public void StartBlowing()
        {
            Blowing = true;
            BlowSound.Volume = 0.5f;
            BlowSound.Pitch = BlowSound.Volume / 100;
            BlowSound.Play();
        }

        public void StopBlowing()
        {
            Blowing = false;
            AirTime = 0;
        }

        override public void Update(Level level)
        {
            for (int i = 0; i < Airs.Count; i++)
            {
                Airs[i].Update(level);
                if (Airs[i].Alpha == 0)
                {
                    Airs.RemoveAt(i);
                    i--;
                }
            }
            Vector2i m = Mouse.GetPosition(Game.Window);
            double dX = m.X - level.Center.X;
            double dY = m.Y - level.Center.Y;
            float a;
            if (dX >= 0)
            {
                a = (float)(Math.Atan(dY / dX) - Math.PI / 2);
            }
            else
            {
                a = (float)(Math.Atan(dY / dX) + Math.PI / 2);
            }
            Angle = (float)(a / Math.PI * 180);
            Sprite.Rotation = Angle;
            Sprite.Position = new Vector2f(m.X, m.Y);
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (!TogglingBlow)
                {
                    TogglingBlow = true;
                    if (!Blowing)
                    {
                        StartBlowing();
                    }
                    else
                    {
                        StopBlowing();
                    }
                }
            }
            else
            {
                TogglingBlow = false;
            }
            if (Blowing)
            {
                AirTime--;
                if (AirTime <= 0)
                {
                    Airs.Add(new LeafblowerAir(Angle, Sprite.Position.X, Sprite.Position.Y));
                    AirTime = 10;
                }
                if (BlowSound.Volume < 100)
                {
                    BlowSound.Volume += 10;
                    BlowSound.Pitch = BlowSound.Volume / 200 + 0.5f;
                }
            }
            else
            {
                if (BlowSound.Status == SoundStatus.Playing)
                {
                    if (BlowSound.Volume > 0)
                    {
                        BlowSound.Volume -= 5;
                        BlowSound.Pitch = BlowSound.Volume * 0.008f + 0.2f;
                    }
                    else
                    {
                        BlowSound.Stop();
                    }
                }
            }
        }

        override public void Draw()
        {
            for (int i = 0; i < Airs.Count; i++)
            {
                Airs[i].Draw();
            }
            Game.Window.Draw(Sprite);
        }
    }

    class LeafblowerAir : Entity
    {
        private float Offset;
        public byte Alpha;

        public LeafblowerAir(float angle, float x, float y)
        {
            Sprite = new Sprite(Resources.Textures["air"]);
            Offset = 20;
            Sprite.Origin = new Vector2f(53, Offset);
            Sprite.Position = new Vector2f(x, y);
            Sprite.Rotation = angle;
            Alpha = 192;
            Sprite.Color = new Color(255, 255, 255, Alpha);
        }

        public override void Update(Level level)
        {
            Offset += 10;
            Sprite.Origin = new Vector2f(Sprite.Origin.X, Offset);
            Alpha -= 8;
            if (Alpha < 8)
            {
                Alpha = 0;
            }
            Sprite.Color = new Color(255, 255, 255, Alpha);
        }

        public override void Draw()
        {
            Game.Window.Draw(Sprite);
        }
    }
}
