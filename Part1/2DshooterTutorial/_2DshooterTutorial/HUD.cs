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
    public class HUD
    {
        public int playerScore, screenWidth, screenHeight;
        public SpriteFont playerScoreFont;
        public Vector2 playerScorePos;
        public bool showHud;

        //constructor
        public HUD()
        {
            playerScore = 0;
            showHud = true;
            screenHeight = 950;
            screenWidth = 800;
            playerScoreFont = null;
            //playerScorePos = new Vector2(screenWidth / 2, 50);
            playerScorePos = new Vector2(500, 100);
        }

        //load content
        public void LoadContent(ContentManager Content)
        {
            playerScoreFont = Content.Load<SpriteFont>("georgia");
        }

        //update
        public void Update(GameTime gameTime)
        {
            //get keyboard state
            KeyboardState keyState = Keyboard.GetState();
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            //if we are showing our hud(if showhud==true) then display our hud
            if (showHud)
                spriteBatch.DrawString(playerScoreFont, "Score - "+ playerScore, playerScorePos, Color.Red);
        }
    }
}
