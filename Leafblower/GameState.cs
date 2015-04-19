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
            ClickText = new Text("Click anywhere to start playing", Resources.Font, 30);
            ClickText.Color = Color.Black;
            ClickText.Position = new Vector2f(430, 400);
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
            Level = new Level(this, LevelID);
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
        private int FadeOutTime;

        public EndState()
        {
            TitleText1 = new Text("Leaf", Resources.Font, 200);
            TitleText1.Color = new Color(75, 196, 56);
            TitleText1.Position = new Vector2f(80, 20);
            TitleText2 = new Text("Blower", Resources.Font, 200);
            TitleText2.Color = new Color(72, 126, 234);
            TitleText2.Position = new Vector2f(200, 120);
            ClickText = new Text("You beat the game.", Resources.Font, 30);
            ClickText.Color = Color.Black;
            ClickText.Position = new Vector2f(430, 400);
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
}
