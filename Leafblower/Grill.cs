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
    class Grill : CollectionDevice
    {
        private List<Bacon> Bacons;
        private Sound RoastSound;

        public Grill()
        {
            Sprite = new Sprite(Resources.Textures["grill"]);
            HitPoint = new Vector2f(400, 225);
            HitRadius = 60;
            Sprite.Origin = new Vector2f(100, 70);
            Sprite.Position = HitPoint;
            Bacons = new List<Bacon>();
            RoastSound = Resources.Sounds["roast"];
        }

        protected override void Collect(Entity enemy)
        {
            Collection++;
            float x = enemy.HitPoint.X;
            float y = enemy.HitPoint.Y;
            enemy.BeCollected(new Vector2f(x, y));
            Bacons.Add(new Bacon(x, y));
            RoastSound.Play();
        }

        protected override void UpdateAnimations(Level level)
        {
            for (int i = 0; i < Bacons.Count; i++)
            {
                Bacons[i].Update(level);
                if (Bacons[i].Alpha == 0)
                {
                    Bacons.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void DrawBelow()
        {
            Game.Window.Draw(Sprite);
            for (int i = 0; i < Bacons.Count; i++)
            {
                Bacons[i].Draw();
            }
        }

        public override void DrawAbove()
        {
            // smoke?
        }
    }

    class Bacon : Entity
    {
        public byte Alpha;
        private int DisappearTime;

        public Bacon(float x, float y)
        {
            Sprite = new Sprite(Resources.Textures["bacon"]);
            Sprite.Origin = new Vector2f(60, 20);
            Sprite.Position = new Vector2f(x, y);
            Alpha = 1;
            Sprite.Color = new Color(255, 255, 255, Alpha);
            DisappearTime = 1000;
        }

        public override void Update(Level level)
        {
            DisappearTime -= 8;
            if (DisappearTime < 255)
            {
                Alpha = (byte)DisappearTime;
                Sprite.Color = new Color(255, 255, 255, Alpha);
            }
            else if (Alpha < 255)
            {
                Alpha += 8;
                if (Alpha > 255 - 8)
                {
                    Alpha = 255;
                }
                Sprite.Color = new Color(255, 255, 255, Alpha);
            }
        }

        public override void Draw()
        {
            Game.Window.Draw(Sprite);
        }
    }
}
