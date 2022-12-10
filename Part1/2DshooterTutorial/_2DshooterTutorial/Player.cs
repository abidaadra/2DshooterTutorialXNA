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
{//Main
   public class Player
    {
        public Texture2D texture, bulletTexture,healthTexture;
        public Vector2 position,healthBarPosition;
        public int speed,health;
        public float bulletDelay;
        public Rectangle boundingBox, healthRectangle;
        public bool isColliding;
        public List<Bullet> bulletList;
        SoundManager sm = new SoundManager();

        //Constructor
        public Player()
        {
            bulletList = new List<Bullet>();
            texture = null;
            position = new Vector2(300, 300);
            bulletDelay = 5;
            speed = 10;
            isColliding = false;
            health = 200;
            healthBarPosition = new Vector2(100, 100);
        }

        //Load content
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ship");
            bulletTexture = Content.Load<Texture2D>("playerbullet");
            healthTexture = Content.Load<Texture2D>("healthbar");
            sm.LoadContent(Content);
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            foreach (Bullet b in bulletList)
                b.Draw(spriteBatch);

        }

        //Update
        public void Update(GameTime gameTime)
        {   //Getting Keyboard State
            KeyboardState keyState = Keyboard.GetState();


            //Boundingbox for our playership
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //set rectangle for healthbar
            healthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, health, 25);

            //fire bullets
            if(keyState.IsKeyDown(Keys.Space))
            {
                Shoot();

            }

            UpdateBullets();

            //Ship Controls
            if (keyState.IsKeyDown(Keys.Up))
                position.Y = position.Y - speed;

            if (keyState.IsKeyDown(Keys.Left))
                position.X = position.X - speed;

            if (keyState.IsKeyDown(Keys.Down))
                position.Y = position.Y + speed;

            if (keyState.IsKeyDown(Keys.Right))
                position.X = position.X + speed;

            //Keep Player Ship In Screen Bounds
            if (position.X <= 0)
                position.X = 0;

            if (position.X >= 800 - texture.Width)
                position.X = 800 - texture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= 950 - texture.Height)
                position.Y = 950 - texture.Height;




        }

        //Shoot Method : used to set starting position of our bullets
        public void Shoot()
        {
            //Shoot only if bullet delay resets
            if (bulletDelay >= 0)
                bulletDelay--;

            //If bullet delay is at 0: create new bullet at player position, make it visible on the screen, then add that bullet to that list
            if(bulletDelay<=0)
            {
                sm.playerShootSound.Play();
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.position = new Vector2(position.X + 32 - newBullet.texture.Width / 2, position.Y + 30);

                newBullet.isVisible = true;

                //Making bullet visible
                if (bulletList.Count() < 20)
                    bulletList.Add(newBullet);
            }

            //reset bullet delay
            if (bulletDelay == 0)
                bulletDelay = 5;
        }

        //update bullet function
        public void UpdateBullets()
        {
            //for each bullet in out bulletList: update the movement and if the bullet hits the top of the screen remove it form the list
            foreach(Bullet b in bulletList)
            {
                //Boundingbox for our every bullet in our bulletList
                b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
                
                //set movement for bullet
                b.position.Y = b.position.Y - b.speed;

                //if bullet hits the top of the screen then make visible false
                if (b.position.Y <= 0)
                    b.isVisible = false;

            }

            //Iterate through bulletlist and see if any of the bullets are not visible, if they arent then remove that bullet from our bullet list
            for(int i=0;i<bulletList.Count;i++)
            {
                if(!bulletList[i].isVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
