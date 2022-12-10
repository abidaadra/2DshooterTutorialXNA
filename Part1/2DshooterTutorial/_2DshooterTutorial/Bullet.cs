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
    //Main
    public class Bullet
    {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 origin;
        public Vector2 position;
        public bool isVisible;
        public float speed;


        //Constuctor
        public Bullet(Texture2D newTexture)
        {
            speed = 10;
            texture = newTexture;
            isVisible = false;
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
