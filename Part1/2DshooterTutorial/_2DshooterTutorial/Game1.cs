using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _2DshooterTutorial
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //state enum
        public enum State
        {
            Menu,
            Playing,
            Gameover

        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        public int enemyBulletDamage;
        public Texture2D menuImage;
        public Texture2D gameoverImage;

        //list
        List<Asteroid> asteroidList = new List<Asteroid>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Explosion> explosionList = new List<Explosion>();
        //instantiating  our player and starfield objects
        Player p = new Player();
        Starfield sf = new Starfield();
        HUD hud = new HUD();
        SoundManager sm = new SoundManager();

        //set first state
        State gameState = State.Menu;

        //Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 950;
            this.Window.Title = "XNA - 2D Space Shooter Tutorial ";
            Content.RootDirectory = "Content";
            enemyBulletDamage = 10;
            menuImage = null;
            gameoverImage = null;
        }

        //Init
        protected override void Initialize()
        {
           

            base.Initialize();
        }

        //Load Content
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hud.LoadContent(Content);
            p.LoadContent(Content);
            sf.LoadContent(Content);
            sm.LoadContent(Content);
            menuImage = Content.Load<Texture2D>("menuimage");
            gameoverImage = Content.Load<Texture2D>("gameover");
        }

       //Unload content
        protected override void UnloadContent()
        {
            
        }

        //update
        protected override void Update(GameTime gameTime)
        {
            //allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //updating playing state
            switch(gameState)
            {
                case State.Playing:
                    {

                        
                        sf.speed = 5;
                        //updating enemy's and checking collision of enemyship to playership
                        foreach (Enemy e in enemyList)
            {
                //check if enemyship is colliding with player
                if(e.boundingBox.Intersects(p.boundingBox))
                {
                    p.health -= 40;
                    e.isVisible = false;
                }

                //check enemy bullet collision with player ship
                for(int i=0;i<e.bulletList.Count;i++)
                {
                    if(p.boundingBox.Intersects(e.bulletList[i].boundingBox))
                    {
                        p.health -= enemyBulletDamage;
                        e.bulletList[i].isVisible = false;
                    }
                }

                //check player bullet collision to enemy ship
                for(int i=0;i<p.bulletList.Count;i++)
                {
                    if(p.bulletList[i].boundingBox.Intersects(e.boundingBox))
                    {
                        sm.explodeSound.Play();
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(e.position.X, e.position.Y)));
                        hud.playerScore += 20;
                        p.bulletList[i].isVisible = false;
                        e.isVisible = false;
                    }
                }

                e.Update(gameTime);
            }
            foreach(Explosion ex in explosionList)
            {
                ex.Update(gameTime);
            }

            //for each asteroid in our asteroidlist, update it and check for collisions
            foreach(Asteroid a in asteroidList)
            {
                //check to see if any of the asteroids are colliding with our playership,if they are.. set isVisible to false(remove them from the asteroidlist) 
                if(a.boundingBox.Intersects(p.boundingBox))
                {
                    p.health -= 20;
                    a.isVisible = false;
                }

                //iterate through our bulletlist if any asteroids come in contact with these bullets, destroy bullet and asteroids
                for(int i = 0;i<p.bulletList.Count;i++)
                {
                    if(a.boundingBox.Intersects(p.bulletList[i].boundingBox))
                    {
                        sm.explodeSound.Play();
                        explosionList.Add(new Explosion(Content.Load<Texture2D>("explosion3"), new Vector2(a.position.X, a.position.Y)));
                        hud.playerScore += 5;
                        a.isVisible = false;
                        p.bulletList.ElementAt(i).isVisible = false;
                    }
                }

                a.Update(gameTime);
            }

            //if playerhealth hits 0 then go to gameoverstate
            if(p.health<=0)
                        {
                            gameState = State.Gameover;
                        }
            LoadAsteroids();
            LoadEnemies();
            //hud.Update(gameTime);
            p.Update(gameTime);
            sf.Update(gameTime);
            ManageExplosions();
                        break;

                    }

                    //updating menu state
                case State.Menu:
                    {
                        //get keyboard state
                        KeyboardState keyState = Keyboard.GetState();
                        if(keyState.IsKeyDown(Keys.Enter))
                        {
                            gameState = State.Playing;
                            MediaPlayer.Play(sm.bgMusic);

                        }
                        sf.Update(gameTime);
                        sf.speed = 1;
                        if(keyState.IsKeyDown(Keys.Back))
                        {
                            this.Exit();
                        }
                        break;
                    }

                    //updating gameover state
                case State.Gameover:
                    {
                        //get keyboard state
                        KeyboardState keyState = Keyboard.GetState();

                        //if in the gameover screen and user hits "escape" key, return to the menu
                        if(keyState.IsKeyDown(Keys.Escape))
                        {
                            p.position = new Vector2(300, 300);
                            enemyList.Clear();
                            asteroidList.Clear();
                            p.health = 200;
                            hud.playerScore = 0;
                            gameState = State.Menu;
                        }
                        //Stop music
                        MediaPlayer.Stop();

                        break;
                    }

            }


           
            base.Update(gameTime);
        }

        //Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch(gameState)
            {//drawing playing state
                case State.Playing:
                    {
                        sf.Draw(spriteBatch);

                        p.Draw(spriteBatch);

                        foreach (Explosion ex in explosionList)
                        {
                            ex.Draw(spriteBatch);
                        }
                        foreach (Asteroid a in asteroidList)
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Enemy e in enemyList)
                        {
                            e.Draw(spriteBatch);
                        }
                        hud.Draw(spriteBatch);
                        break;
                    }

                    //drawing menu state
                case State.Menu:
                    {
                        sf.Draw(spriteBatch);
                        spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                        break;
                    }

                    //drawing menu state
                case State.Gameover:
                    {
                        spriteBatch.Draw(gameoverImage, new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(hud.playerScoreFont, "Your Final Score was - " + hud.playerScore.ToString(), new Vector2(235, 100), Color.Red);
                        break;
                    }
            }

          

            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //load asteroids
        public void LoadAsteroids()
        {
            //Creating random variables for our x and y axis of our asteroids
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            //if there are less than 5 asteroids on the screen, then create more until there is 5 again
            if(asteroidList.Count()<5)
            {
                asteroidList.Add(new Asteroid(Content.Load<Texture2D>("asteroid"),new Vector2(randX,randY)));
            }

            //if any of these asteroids in the list were destroyed, then remove them from list
            for(int i=0; i<asteroidList.Count;i++)
            {
                if(!asteroidList[i].isVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }
        }

        //load enemies
        public void LoadEnemies()
        {
            //Creating random variables for our x and y axis of our asteroids
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            //if there are less than 3 enemies on the screen, then create more until there is 5 again
            if (enemyList.Count() < 3)
            {
                enemyList.Add(new Enemy(Content.Load<Texture2D>("enemyship"), new Vector2(randX, randY), Content.Load<Texture2D>("EnemyBullet")));
            }

            //if any of these asteroids in the list were destroyed, then remove them from list
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        //manage explosions
        public void ManageExplosions()
        {
            for(int i=0;i<explosionList.Count;i++)
            {
                if(!explosionList[i].isVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }

    }
}
