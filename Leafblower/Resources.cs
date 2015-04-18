using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;

namespace Leafblower
{
    static class Resources
    {
        public static Dictionary<String, Texture> Textures;
        public static Font Font;

        public static void Load()
        {
            Textures = new Dictionary<string, Texture>();
            Textures.Add("leafblower", new Texture("res/leafblower.png"));
            Textures.Add("air", new Texture("res/air.png"));
            Textures.Add("pavement", new Texture("res/pavement.png"));
            Textures.Add("basket", new Texture("res/basket.png"));
            Textures.Add("leaf1", new Texture("res/leaf1.png"));
            Textures.Add("leaf2", new Texture("res/leaf2.png"));
            Textures.Add("dirt", new Texture("res/dirt.png"));
            Textures.Add("magnifier", new Texture("res/magnifier.png"));
            Textures.Add("ant", new Texture("res/ant.png"));
            Textures.Add("smoke", new Texture("res/smoke.png"));
            for (int i = 0; i < Textures.Count; i++)
            {
                Textures.ElementAt(i).Value.Smooth = true;
            }
            Font = new Font("res/Bangers.ttf");
        }
    }
}
