using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Bin : CollectionDevice
    {
        public Bin()
        {
            Sprite = new Sprite(Resources.Textures["bin"]);
            HitPoint = new Vector2f(70, 380);
            HitRadius = 30;
            Sprite.Origin = new Vector2f(32, 30);
            Sprite.Position = HitPoint;
        }

        protected override void Collect(Entity enemy)
        {
            Collection++;
            enemy.BeCollected(HitPoint);
        }

        protected override void UpdateAnimations(Level level) { }

        public override void DrawBelow()
        {
            Game.Window.Draw(Sprite);
        }

        public override void DrawAbove() { }
    }
}
