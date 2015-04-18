using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Level
    {
        private LevelState GameState;
        private Sprite Background;
        private CollectionDevice Collector;
        private int Collection;
        private int CollectionTarget;
        private Text CollectionText;
        private int CollectionWhiteTime;
        private RectangleShape FadeRectangle;
        private int FadeOutTime;
        private int FadeInTime;
        public bool Won;
        public Vector2f Center;
        public Leafblower Leafblower;
        public List<Entity> Enemies;

        public Level(LevelState gameState, int id)
        {
            GameState = gameState;
            Enemies = new List<Entity>();
            if (id == 1)
            {
                Background = new Sprite(Resources.Textures["pavement"]);
                Collector = new Basket();
                CollectionTarget = 50;
                Center = new Vector2f(Game.Width / 2, 0);
                for (int i = 0; i < 100; i++)
                {
                    Enemies.Add(new Leaf((float)(Game.Random.NextDouble() * 0.8 + 0.1) * Game.Width, (float)(Game.Random.NextDouble() * 0.6 + 0.3) * Game.Height));
                }
            }
            else if (id == 0)
            {
                Background = new Sprite(Resources.Textures["dirt"]);
                Collector = new Magnifier();
                CollectionTarget = 25;
                Center = new Vector2f(520, 275);
                for (int i = 0; i < 100; i++)
                {
                    float x, y;
                    float dX, dY;
                    do
                    {
                        x = (float)Game.Random.NextDouble() * Game.Width;
                        y = (float)Game.Random.NextDouble() * Game.Height;
                        dX = x - Center.X;
                        dY = y - Center.Y;
                    }
                    while (dX * dX + dY * dY < 2500);
                    Enemies.Add(new Ant(x, y));
                }
            }
            Leafblower = new Leafblower(Center);
            CollectionText = new Text(String.Format("0/{0}", CollectionTarget), Resources.Font, 50);
            CollectionText.Position = new Vector2f(30, 30);
            CollectionText.Color = Color.Black;
            FadeRectangle = new RectangleShape(new Vector2f(Game.Width, Game.Height));
            FadeInTime = 255;
            FadeRectangle.FillColor = new Color(255, 255, 255, (byte)FadeInTime);
        }

        public void Update()
        {
            Leafblower.Update(this);
            if (!Won)
            {
                if (FadeInTime > 0)
                {
                    FadeInTime -= 4;
                    Console.WriteLine(FadeInTime);
                    if (FadeOutTime < 0)
                    {
                        FadeOutTime = 0;
                    }
                    FadeRectangle.FillColor = new Color(255, 255, 255, (byte)FadeInTime);
                }

                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].Update(this);
                }
                Collector.Update(this);
                if (Collection != Collector.Collection)
                {
                    Collection = Collector.Collection;
                    CollectionText.DisplayedString = String.Format("{0}/{1}", Collection, CollectionTarget);
                    CollectionText.Color = new Color(192, 192, 192);
                    CollectionWhiteTime = 16;
                    if (Collection >= CollectionTarget)
                    {
                        Won = true;
                        FadeOutTime += 4;
                        FadeRectangle.FillColor = new Color(255, 255, 255, (byte)FadeOutTime);
                    }
                }
            }
            else
            {
                FadeOutTime += 4;
                if (FadeOutTime > 255)
                {
                    FadeOutTime = 255;
                    GameState.NextLevel();
                }
                FadeRectangle.FillColor = new Color(255, 255, 255, (byte) FadeOutTime);
            }
            if (CollectionWhiteTime > 0)
            {
                CollectionWhiteTime--;
                if (CollectionWhiteTime <= 0)
                {
                    CollectionText.Color = Color.Black;
                }
                else
                {
                    byte c = (byte)(CollectionWhiteTime * 12);
                    CollectionText.Color = new Color(c, c, c);
                }
            }
        }

        public void Draw()
        {
            Game.Window.Draw(Background);
            if (Collector.Below)
            {
                Collector.Draw();
            }
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw();
            }
            if (!Collector.Below)
            {
                Collector.Draw();
            }
            Leafblower.Draw();
            Game.Window.Draw(CollectionText);
            if (Won || FadeInTime > 0)
            {
                Game.Window.Draw(FadeRectangle);
            }
        }
    }
}
