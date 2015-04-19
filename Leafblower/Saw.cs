using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Saw : CollectionDevice
    {
        private Texture Texture;
        private IntRect[] AnimationFrames;
        private int Frame;
        private int FrameTime;
        private List<Blood> Bloods;
        
        public Saw()
        {
            Texture = Resources.Textures["saw"];
            AnimationFrames = new IntRect[] { new IntRect(0, 0, 76, 59), new IntRect(76, 0, 76, 59) };
            Frame = 0;
            Sprite = new Sprite(Texture, AnimationFrames[Frame]);
            HitPoint = new Vector2f(39, 233);
            HitRadius = 50;
            Sprite.Origin = new Vector2f(38, 30);
            Sprite.Position = HitPoint;
            Bloods = new List<Blood>();
        }

        protected override void Collect(Entity enemy)
        {
            Collection++;
            enemy.BeCollected(HitPoint);
            Bloods.Add(new Blood(HitPoint.X, HitPoint.Y));
        }

        protected override void UpdateAnimations(Level level)
        {
            FrameTime++;
            if (FrameTime > 5)
            {
                Frame++;
                if (Frame > 1)
                {
                    Frame = 0;
                }
                FrameTime = 0;
                Sprite.TextureRect = AnimationFrames[Frame];
            }
            for (int i = 0; i < Bloods.Count; i++)
            {
                Bloods[i].Update(level);
                if (Bloods[i].Alpha == 0)
                {
                    Bloods.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void DrawBelow()
        {
            Game.Window.Draw(Sprite);
        }

        public override void DrawAbove()
        {
            for (int i = 0; i < Bloods.Count; i++)
            {
                Bloods[i].Draw();
            }
        }
    }
}
