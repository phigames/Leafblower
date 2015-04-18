using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace Leafblower
{
    abstract class GameState
    {
        public abstract void Update();

        public abstract void Draw();
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
}
