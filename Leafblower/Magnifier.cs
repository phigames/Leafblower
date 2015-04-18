using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Magnifier : CollectionDevice
    {
        private List<Smoke> Smokes;

        public Magnifier()
        {
            Sprite = new SFML.Graphics.Sprite(Resources.Textures["magnifier"]);
            HitPoint = new SFML.Window.Vector2f(520, 275);
            HitRadius = 30;
            Sprite.Origin = new SFML.Window.Vector2f(125, 335);
            Sprite.Position = HitPoint;
            Below = false;
            Smokes = new List<Smoke>();
        }

        protected override void Collect(Entity enemy)
        {
            Collection++;
            enemy.BeCollected(HitPoint);
            Smokes.Add(new Smoke(HitPoint.X, HitPoint.Y));
        }

        protected override void CustomUpdate(Level level)
        {
            for (int i = 0; i < Smokes.Count; i++)
            {
                Smokes[i].Update(level);
                if (Smokes[i].Alpha == 0)
                {
                    Smokes.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void CustomDraw()
        {
            for (int i = 0; i < Smokes.Count; i++)
            {
                Smokes[i].Draw();
                Console.WriteLine("smoke");
            }
        }
    }

    class Smoke : Entity
    {
        private float Offset;
        public byte Alpha;

        public Smoke(float x, float y)
        {
            Sprite = new SFML.Graphics.Sprite(Resources.Textures["smoke"]);
            Offset = 100;
            Sprite.Origin = new Vector2f(24, Offset);
            Sprite.Position = new Vector2f(x, y);
            Alpha = 255;
            Sprite.Color = new Color(255, 255, 255, Alpha);
        }

        public override void Update(Level level)
        {
            Offset += 3;
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