using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Saturn : Entity
    {
        Vector2f Speed;
        float RotationSpeed;

        public Saturn(float x, float y)
        {
            Sprite = new Sprite(Resources.Textures["saturn"]);
            HitPoint = new Vector2f(x, y);
            HitRadius = 64;
            Sprite.Origin = new Vector2f(116, 64);
            Sprite.Position = HitPoint;
            Sprite.Rotation = (float) Game.Random.NextDouble() * 360;
            RotationSpeed = 100;
        }

        public override void Update(Level level)
        {
            if (!Collected)
            {
                if (level.Leafblower.Blowing && level.Leafblower.InBlowingRange(HitPoint.X, HitPoint.Y))
                {
                    Speed = level.Leafblower.GetSpeedVector(HitPoint.X, HitPoint.Y);
                }
                else
                {
                    Speed *= 0.99f;
                }
                HitPoint += Speed * 100;
                if (HitPoint.X < HitRadius)
                {
                    HitPoint = new Vector2f(HitRadius, HitPoint.Y);
                }
                else if (HitPoint.X > Game.Width - HitRadius)
                {
                    HitPoint = new Vector2f(Game.Width - HitRadius, HitPoint.Y);
                }
                if (HitPoint.Y < HitRadius)
                {
                    HitPoint = new Vector2f(HitPoint.X, HitRadius);
                }
                else if (HitPoint.Y > Game.Height - HitRadius)
                {
                    HitPoint = new Vector2f(HitPoint.X, Game.Height - HitRadius);
                }
                Sprite.Position = HitPoint;
                Sprite.Rotation += (Speed.X + Speed.Y) * RotationSpeed;
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
