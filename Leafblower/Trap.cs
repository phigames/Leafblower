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
    class Trap : CollectionDevice
    {
        private Texture Texture;
        private IntRect[] AnimationFrames;
        private int Frame;
        private int FrameTime;
        private List<Blood> Bloods;
        private Sound CatchSound;

        public Trap()
        {
            Texture = Resources.Textures["trap"];
            AnimationFrames = new IntRect[] { new IntRect(0, 0, 244, 182), new IntRect(244, 0, 244, 182) };
            Frame = 0;
            Sprite = new Sprite(Texture, AnimationFrames[Frame]);
            HitPoint = new Vector2f(205, 190);
            HitRadius = 80;
            Sprite.Origin = new Vector2f(165, 105);
            Sprite.Position = HitPoint;
            Bloods = new List<Blood>();
            CatchSound = Resources.Sounds["trap"];
        }

        protected override void Collect(Entity enemy)
        {
            Collection++;
            enemy.BeCollected(HitPoint);
            Frame = 1;
            Sprite.TextureRect = AnimationFrames[Frame];
            Bloods.Add(new Blood(HitPoint.X, HitPoint.Y));
            CatchSound.Play();
        }

        protected override void UpdateAnimations(Level level)
        {
            if (Frame != 0)
            {
                FrameTime++;
                if (FrameTime > 20)
                {
                    Frame = 0;
                    FrameTime = 0;
                    Sprite.TextureRect = AnimationFrames[Frame];
                }
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

    class Blood : Entity
    {
        private Texture Texture;
        private IntRect[] AnimationFrames;
        private int Frame;
        private int FrameTime;
        public byte Alpha;

        public Blood(float x, float y)
        {
            Texture = Resources.Textures["blood"];
            AnimationFrames = new IntRect[] { new IntRect(0, 0, 237, 172), new IntRect(237, 0, 237, 172), new IntRect(474, 0, 237, 172) };
            Frame = 0;
            FrameTime = 0;
            Sprite = new Sprite(Texture, AnimationFrames[Frame]);
            Sprite.Origin = new Vector2f(120, 85);
            Sprite.Position = new Vector2f(x, y);
            Sprite.Rotation = (float) Game.Random.NextDouble() * 360;
            Alpha = 255;
            Sprite.Color = new Color(255, 255, 255, Alpha);
        }

        public override void Update(Level level)
        {
            if (Frame < 2)
            {
                FrameTime++;
                if (FrameTime > 5)
                {
                    Frame++;
                    FrameTime = 0;
                }
                Sprite.TextureRect = AnimationFrames[Frame];
            }
            else
            {
                Alpha -= 2;
                if (Alpha < 2)
                {
                    Alpha = 0;
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
