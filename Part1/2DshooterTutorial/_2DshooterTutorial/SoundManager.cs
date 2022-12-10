using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _2DshooterTutorial
{
    public class SoundManager
    {
        public SoundEffect playerShootSound;
        public SoundEffect explodeSound;
        public Song bgMusic;

        //constructor
        public SoundManager()
        {
            playerShootSound = null;
            explodeSound = null;
            bgMusic = null;

        }

        public void LoadContent(ContentManager Content)
        {
            playerShootSound = Content.Load<SoundEffect>("playershoot");
            explodeSound = Content.Load<SoundEffect>("explode");
            bgMusic = Content.Load<Song>("theme");
        }
    }
}
