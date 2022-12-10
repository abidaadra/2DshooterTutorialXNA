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
   public class Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public float timer;
        public float interval;
        public Vector2 origin;
        public int currentFrame, spriteWidth, spriteHeight;
        public Rectangle sourceRect;
        public bool isVisible;

        //Constructor
        public Explosion(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            timer = 0f;
            interval = 20f;
            currentFrame = 1;
            spriteWidth = 128;
            spriteHeight = 128;
            isVisible = true;
        }

        //load content
        public void LoadContent(ContentManager Content)
        {

        }

        //update
        public void Update(GameTime gameTime)
        {
            //increase the timer by the number of milliseconds since update was last called
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            //Check the time is more than the chosen interval
            if(timer>interval)
            {
                //show next frame
                currentFrame++;
                //reset timer
                timer = 0f;
            }

            //if were on last frame, make the explosion invisible and reset the current frame inthe beginning of the spritesheet
            if(currentFrame==17)
            {
                isVisible = false;
                currentFrame = 0;


            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            //if visible then draw
            if (isVisible == true)
                spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f, origin,1.0f, SpriteEffects.None, 0);


        }
    }
}
