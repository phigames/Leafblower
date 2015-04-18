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
        public bool Below;
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
            CustomUpdate(level);
        }

        protected virtual void CustomUpdate(Level level) { }

        public override void Draw()
        {
            Game.Window.Draw(Sprite);
            CustomDraw();
        }

        protected virtual void CustomDraw() { }
    }
}
