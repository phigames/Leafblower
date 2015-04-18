using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Basket : CollectionDevice
    {
        public Basket()
        {
            Sprite = new Sprite(Resources.Textures["basket"]);
            HitPoint = new Vector2f(Game.Width / 2, 51);
            HitRadius = 51;
            Sprite.Origin = new Vector2f(55, 59);
            Sprite.Position = HitPoint;
        }

        protected override void Collect(Entity enemy)
        {
            Collection++;
            enemy.BeCollected(new Vector2f(HitPoint.X + (float) (Game.Random.NextDouble() - 0.5) * 40, HitPoint.Y + (float) (Game.Random.NextDouble() - 0.5) * 40));
        }
    }
}
