using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    abstract class Entity
    {
        protected Sprite Sprite;
        protected Vector2f HitPoint;
        protected float HitRadius;
        public bool Collected;

        public bool Intersects(Entity other)
        {
            //float t = HitRadius + other.HitRadius;
            float t = Math.Max(HitRadius, other.HitRadius);
            float dx = HitPoint.X - other.HitPoint.X;
            float dy = HitPoint.Y - other.HitPoint.Y;
            double d = dx * dx + dy * dy;
            return d <= t * t;
        }

        public void BeCollected(Vector2f target)
        {
            Collected = true;
            HitPoint = target;
            Sprite.Position = target;
        }

        public abstract void Update(Level level);

        public abstract void Draw();
    }
}
