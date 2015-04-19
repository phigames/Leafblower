using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Audio;

namespace Leafblower
{
    static class Resources
    {
        public static Dictionary<string, Texture> Textures;
        public static Font Font;
        public static Dictionary<string, Sound> Sounds;

        public static void Load()
        {
            Textures = new Dictionary<string, Texture>();
            Textures.Add("leafblower", new Texture("res/leafblower.png"));
            Textures.Add("air", new Texture("res/air.png"));
            // Level 1
            Textures.Add("pavement", new Texture("res/pavement.png"));
            Textures.Add("basket", new Texture("res/basket.png"));
            Textures.Add("leaf1", new Texture("res/leaf1.png"));
            Textures.Add("leaf2", new Texture("res/leaf2.png"));
            // Level 2
            Textures.Add("dirt", new Texture("res/dirt.png"));
            Textures.Add("magnifier", new Texture("res/magnifier.png"));
            Textures.Add("ant", new Texture("res/ant.png"));
            Textures.Add("smoke", new Texture("res/smoke.png"));
            // Level 3
            Textures.Add("wood", new Texture("res/wood.png"));
            Textures.Add("trap", new Texture("res/trap.png"));
            Textures.Add("mouse", new Texture("res/mouse.png"));
            Textures.Add("blood", new Texture("res/blood.png"));
            // Level 4
            Textures.Add("metal", new Texture("res/metal.png"));
            Textures.Add("grill", new Texture("res/grill.png"));
            Textures.Add("pig", new Texture("res/pig.png"));
            Textures.Add("bacon", new Texture("res/bacon.png"));
            // Level 5
            Textures.Add("city", new Texture("res/city.png"));
            Textures.Add("saw", new Texture("res/saw.png"));
            Textures.Add("human", new Texture("res/human.png"));
            // Level 6
            Textures.Add("sky", new Texture("res/sky.png"));
            Textures.Add("bin", new Texture("res/bin.png"));
            Textures.Add("earth", new Texture("res/earth.png"));
            Textures.Add("saturn", new Texture("res/saturn.png"));
            Textures.Add("milkyway", new Texture("res/milkyway.png"));
            Textures.Add("star", new Texture("res/star.png"));
            for (int i = 0; i < Textures.Count; i++)
            {
                Textures.ElementAt(i).Value.Smooth = true;
            }
            Font = new Font("res/Bangers.ttf");
            Sounds = new Dictionary<string, Sound>();
            Sounds.Add("blow", new Sound(new SoundBuffer("res/blow.wav")));
            Sounds["blow"].Loop = true;
            Sounds.Add("trap", new Sound(new SoundBuffer("res/trap.wav")));
            Sounds.Add("sizzle", new Sound(new SoundBuffer("res/sizzle.wav")));
            Sounds.Add("roast", new Sound(new SoundBuffer("res/roast.wav")));
            Sounds.Add("fart", new Sound(new SoundBuffer("res/fart.wav")));
        }
    }
}
