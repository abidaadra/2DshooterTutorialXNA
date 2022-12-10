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
    public class Asteroid
    {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotationAngle;
        public int speed;
        //public bool isColliding, destroyed;

        public bool isVisible;
        Random random = new Random();
        public float randX, randY;


        //Constructor
        public Asteroid(Texture2D newTexture, Vector2 newPosition)
        {
            position = newPosition;
            texture = newTexture;
            speed = 4;
            isVisible = true;
            randX = random.Next(0, 750);
            randY = random.Next(-600, -50);

            //isColliding = false;
            //destroyed = false;
        }

        //Load Content
        public void LoadContent(ContentManager Content)
        {
            //texture = Content.Load<Texture2D>("asteroid");
          

        }

        //Update
        public void Update(GameTime gameTime)
        {
            //Set bounding box for collision
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 45, 45);

            //Updating origin for rotation
            //origin.X = texture.Width / 2;
            //origin.Y = texture.Height / 2;

            //Update movement
            position.Y = position.Y + speed;
            if (position.Y >= 950)
                position.Y = -50;

            //Rotate Asteroid
            //float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //rotationAngle += elapsed;
            //float circle = MathHelper.Pi * 2;
            //rotationAngle = rotationAngle % circle;


        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.Draw(texture, position,Color.White);
        }
    }
}
