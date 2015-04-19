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
        private Sprite StartImage;
        private string StartMessage;
        private Text StartMessageText;
        private int StartMessageTime;
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
        private Text GameTimeText;

        public Level(LevelState gameState, int id)
        {
            GameState = gameState;
            Enemies = new List<Entity>();
            bool d = false;
            if (id == 0)
            {
                StartImage = new Sprite(Resources.Textures["leafblower"]);
                StartImage.Origin = new Vector2f(23, 73);
                StartImage.Rotation = 90;
                StartMessage = "You're a Leaf Blower. Leaf Blowers blow leaves.";
                Background = new Sprite(Resources.Textures["pavement"]);
                Collector = new Basket();
                CollectionTarget = 100;
                Center = new Vector2f(Game.Width / 2, 0);
                for (int i = 0; i < 100; i++)
                {
                    Enemies.Add(new Leaf((float)(Game.Random.NextDouble() * 0.8 + 0.1) * Game.Width, (float)(Game.Random.NextDouble() * 0.6 + 0.3) * Game.Height));
                }
            }
            else if (id == 1)
            {
                StartImage = new Sprite(Resources.Textures["leaf2"]);
                StartImage.Origin = new Vector2f(18, 38);
                StartMessage = "How boring. Let's try something else...";
                Background = new Sprite(Resources.Textures["dirt"]);
                Collector = new Magnifier();
                CollectionTarget = 30;
                Center = new Vector2f(520, 275);
                for (int i = 0; i < 30; i++)
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
                    while (dX * dX + dY * dY < 6400);
                    Enemies.Add(new Ant(x, y));
                }
            }
            else if (id == 2)
            {
                StartImage = new Sprite(Resources.Textures["ant"], new IntRect(0, 0, 43, 39));
                StartImage.Origin = new Vector2f(22, 20);
                StartMessage = "This is too easy for you.";
                Background = new Sprite(Resources.Textures["dirt"]);
                Background = new Sprite(Resources.Textures["wood"]);
                Collector = new Trap();
                CollectionTarget = 20;
                Center = new Vector2f(205, 190);
                for (int i = 0; i < 20; i++)
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
                    while (dX * dX + dY * dY < 10000);
                    Enemies.Add(new Rat(x, y));
                }
            }
            else if (id == 3)
            {
                StartImage = new Sprite(Resources.Textures["mouse"], new IntRect(0, 0, 181, 63));
                StartImage.Origin = new Vector2f(91, 32);
                StartMessage = "What's next, you ask?";
                Background = new Sprite(Resources.Textures["metal"]);
                Collector = new Grill();
                CollectionTarget = 20;
                Center = new Vector2f(400, 225);
                for (int i = 0; i < 20; i++)
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
                    while (dX * dX + dY * dY < 14400);
                    Enemies.Add(new Pig(x, y));
                }
            }
            else if (id == 4)
            {
                StartImage = new Sprite(Resources.Textures["pig"], new IntRect(0, 0, 187, 82));
                StartImage.Origin = new Vector2f(94, 41);
                StartMessage = "Let's move on to greater things.";
                Background = new Sprite(Resources.Textures["city"]);
                Collector = new Saw();
                CollectionTarget = 100;
                Center = new Vector2f(39, 233);
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
                    while (!Human.CanGo(x, y) || dX * dX + dY * dY < 10000);
                    Enemies.Add(new Human(x, y));
                }
            }
            else if (id == 5)
            {
                StartImage = new Sprite(Resources.Textures["human"], new IntRect(47, 0, 31, 29));
                StartImage.Origin = new Vector2f(16, 15);
                StartMessage = "Nothing can stop the almighty Leaf Blower.";
                Background = new Sprite(Resources.Textures["sky"]);
                Collector = new Bin();
                CollectionTarget = 33;
                Center = new Vector2f(70, 380);
                Enemies.Add(new Earth(500, 300));
                Enemies.Add(new Saturn(200, 150));
                Enemies.Add(new Milkyway(650, 100));
                for (int i = 0; i < 30; i++)
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
                    Enemies.Add(new Star(x, y));
                }
            }
            if (!d)
            {
                StartImage.Position = new Vector2f(Game.Width / 2, 150);
                StartImage.Color = new Color(255, 255, 255, (byte)(StartMessageTime * 255 / 50));
                StartMessageText = new Text(StartMessage, Resources.Font, 30);
                StartMessageText.Position = new Vector2f((Game.Width - StartMessageText.GetLocalBounds().Width) / 2, 280);
                StartMessageText.Color = new Color(0, 0, 0, (byte)(StartMessageTime * 255 / 50));
                Leafblower = new Leafblower(Center);
                CollectionText = new Text(String.Format("0/{0}", CollectionTarget), Resources.Font, 50);
                CollectionText.Position = new Vector2f(30, 30);
                CollectionText.Color = Color.Black;
                FadeRectangle = new RectangleShape(new Vector2f(Game.Width, Game.Height));
                FadeInTime = 255;
                FadeRectangle.FillColor = new Color(255, 255, 255, (byte)FadeInTime);
                int t = Game.GameTime / 50;
                int s = t % 60;
                if (s >= 10)
                {
                    GameTimeText = new Text(String.Format("{0}:{1}", t / 60, s), Resources.Font, 50);
                }
                else
                {
                    GameTimeText = new Text(String.Format("{0}:0{1}", t / 60, s), Resources.Font, 50);
                }
                GameTimeText.Position = new Vector2f(650, 30);
                GameTimeText.Color = Color.Black;
            }
        }

        public void Update()
        {
            if (FadeInTime < 255)
            {
                Leafblower.Update(this);
                Game.GameTime++;
                int t = Game.GameTime / 50;
                int s = t % 60;
                if (s >= 10)
                {
                    GameTimeText.DisplayedString = String.Format("{0}:{1}", t / 60, t % 60);
                }
                else
                {
                    GameTimeText.DisplayedString = String.Format("{0}:0{1}", t / 60, t % 60);
                }
            }
            if (!Won)
            {
                if (StartMessageTime < 300)
                {
                    StartMessageTime++;
                    if (StartMessageTime < 50)
                    {
                        StartImage.Color = new Color(255, 255, 255, (byte)(StartMessageTime * 255 / 50));
                        StartMessageText.Color = new Color(0, 0, 0, (byte)(StartMessageTime * 255 / 50));
                    }
                    else if (StartMessageTime > 250)
                    {
                        StartImage.Color = new Color(255, 255, 255, (byte)((300 - StartMessageTime) * 255 / 50));
                        StartMessageText.Color = new Color(0, 0, 0, (byte)((300 - StartMessageTime) * 255 / 50));
                    }
                }
                else if (FadeInTime > 0)
                {
                    FadeInTime -= 4;
                    if (FadeInTime < 0)
                    {
                        FadeInTime = 0;
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
                    CollectionText.Color = new Color(128, 128, 128);
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
                Leafblower.StopBlowing();
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
                    byte c = (byte)(CollectionWhiteTime * 8);
                    CollectionText.Color = new Color(c, c, c);
                }
            }
        }

        public void Draw()
        {
            Game.Window.Draw(Background);
            Collector.DrawBelow();
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw();
            }
            Collector.DrawAbove();
            Leafblower.Draw();
            Game.Window.Draw(CollectionText);
            Game.Window.Draw(GameTimeText);
            if (Won || FadeInTime > 0)
            {
                Game.Window.Draw(FadeRectangle);
            }
            if (StartMessageTime < 300)
            {
                Game.Window.Draw(StartImage);
                Game.Window.Draw(StartMessageText);
            }
        }
    }
}
