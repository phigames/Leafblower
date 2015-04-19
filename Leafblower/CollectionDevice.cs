using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    abstract class CollectionDevice : Entity
    {
        public int Collection;

        protected abstract void Collect(Entity enemy);

        public override void Update(Level level)
        {
            for (int i = 0; i < level.Enemies.Count; i++)
            {
                if (!level.Enemies[i].Collected && level.Enemies[i].Intersects(this))
                {
                    Collect(level.Enemies[i]);
                }
            }
            UpdateAnimations(level);
        }

        protected virtual void UpdateAnimations(Level level) { }

        public override void Draw() { }

        public abstract void DrawBelow();

        public abstract void DrawAbove();
    }
}
