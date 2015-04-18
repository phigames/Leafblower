using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SFML.Window;
using SFML.Graphics;

namespace Leafblower
{
    class Program
    {
        static void Main(string[] args)
        {
            Resources.Load();
            Game.Initialize();
            Game.Start();
        }
    }

    static class Game
    {
        private static bool Running;
        private static Stopwatch Timer;
        private static long LastUpdate;
        private static long UpdateInterval;
        public static uint Width, Height;
        public static RenderWindow Window;
        public static Random Random;
        private static GameState State;

        public static void Initialize()
        {
            Timer = new Stopwatch();
            UpdateInterval = 20;
            Width = 800;
            Height = 450;
            Window = new RenderWindow(new VideoMode(Width, Height), "Leafblower", Styles.Close, new ContextSettings(32, 8, 4));
            Window.SetMouseCursorVisible(false);
            Window.SetFramerateLimit(60);
            Window.Closed += Window_Closed;
            Random = new Random();
            State = new LevelState();
        }

        static void Window_Closed(object sender, EventArgs e)
        {
            Stop();
        }

        public static void Start()
        {
            Running = true;
            Timer.Start();
            LastUpdate = 0;
            Loop();
        }

        public static void Stop()
        {
            Running = false;
            Timer.Stop();
        }

        private static void Loop()
        {
            while (Running)
            {
                Window.DispatchEvents();
                while (Timer.ElapsedMilliseconds - LastUpdate >= UpdateInterval)
                {
                    LastUpdate += UpdateInterval;
                    State.Update();
                }
                Window.Clear(Color.White);
                State.Draw();
                Window.Display();
            }
        }
    }
}
