using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    abstract class GameState
    {
        public abstract void Update();

        public abstract void Draw();
    }

    class MenuState : GameState
    {
        private Text TitleText1;
        private Text TitleText2;
        private Text ClickText;
        private RectangleShape FadeRectangle;
        private int FadeOutTime;

        public MenuState()
        {
            TitleText1 = new Text("Leaf", Resources.Font, 200);
            TitleText1.Color = new Color(75, 196, 56);
            TitleText1.Position = new Vector2f(80, 20);
            TitleText2 = new Text("Blower", Resources.Font, 200);
            TitleText2.Color = new Color(72, 126, 234);
            TitleText2.Position = new Vector2f(200, 120);
            ClickText = new Text("Click anywhere to start the game", Resources.Font, 30);
            ClickText.Color = Color.Black;
            ClickText.Position = new Vector2f(400, 400);
            FadeRectangle = new RectangleShape(new Vector2f(Game.Width, Game.Height));
            FadeOutTime = -1;
        }

        override public void Update()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                FadeOutTime = 0;
            }
            if (FadeOutTime >= 0)
            {
                FadeOutTime += 4;
                if (FadeOutTime > 255 - 4)
                {
                    FadeOutTime = 255;
                    Game.State = new LevelState();
                }
                FadeRectangle.FillColor = new Color(255, 255, 255, (byte)FadeOutTime);
            }
        }

        public override void Draw()
        {
            Game.Window.Draw(TitleText1);
            Game.Window.Draw(TitleText2);
            Game.Window.Draw(ClickText);
            if (FadeOutTime > -1)
            {
                Game.Window.Draw(FadeRectangle);
            }
        }
    }

    class LevelState : GameState
    {
        int LevelID;
        Level Level;

        public LevelState()
        {
            Level = new Level(this, LevelID);
        }

        public void NextLevel()
        {
            LevelID++;
            if (LevelID > 5)
            {
                Game.State = new EndState();
            }
            else
            {
                Level = new Level(this, LevelID);
            }
        }

        override public void Update()
        {
            Level.Update();
        }

        override public void Draw()
        {
            Level.Draw();
        }
    }

    class EndState : GameState
    {
        private Text TitleText1;
        private Text TitleText2;
        private Text ClickText;
        private RectangleShape FadeRectangle;
        private int FadeInTime;
        private int FadeOutTime;

        public EndState()
        {
            TitleText1 = new Text("Universe", Resources.Font, 200);
            TitleText1.Color = new Color(75, 196, 56);
            TitleText1.Position = new Vector2f(80, 20);
            TitleText2 = new Text("Blower", Resources.Font, 200);
            TitleText2.Color = new Color(72, 126, 234);
            TitleText2.Position = new Vector2f(200, 120);
            ClickText = new Text(String.Format("You beat the game in {0} seconds! Wow, that's a lot of seconds!", Game.GameTime / 50), Resources.Font, 30);
            ClickText.Color = Color.Black;
            ClickText.Position = new Vector2f(20, 400);
            FadeRectangle = new RectangleShape(new Vector2f(Game.Width, Game.Height));
            FadeInTime = 255;
            FadeOutTime = -1;
        }

        override public void Update()
        {
            if (FadeInTime > 0)
            {
                FadeInTime -= 4;
                if (FadeInTime < 4)
                {
                    FadeInTime = 0;
                }
                FadeRectangle.FillColor = new Color(255, 255, 255, (byte)FadeInTime);
            }
            else
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    FadeOutTime = 0;
                }
                if (FadeOutTime >= 0)
                {
                    FadeOutTime += 4;
                    if (FadeOutTime > 255 - 4)
                    {
                        FadeOutTime = 255;
                        Game.Stop();
                    }
                    FadeRectangle.FillColor = new Color(255, 255, 255, (byte)FadeOutTime);
                }
            }
        }

        public override void Draw()
        {
            Game.Window.Draw(TitleText1);
            Game.Window.Draw(TitleText2);
            Game.Window.Draw(ClickText);
            if (FadeInTime > 0 || FadeOutTime > -1)
            {
                Game.Window.Draw(FadeRectangle);
            }
        }
    }
}
