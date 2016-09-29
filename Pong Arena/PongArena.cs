using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Pong_Arena
{
    public class PongArena : Game
    {
        private GraphicsDeviceManager graphics;
        private gameStates gameState;
        private SpriteBatch spriteBatch;
        private List<DynamicObject> listDynamicObject = new List<DynamicObject>();
        private List<Object> listObjects = new List<Object>();
        int elapsedBounceTime = 0;
        double rotation = 0;

        //Initialize
        private Object[] arrayObjectAll =
        {
            new Object("ball", new Vector2(100, 100), new Vector2(400, 350),50, 50, 3f),
<<<<<<< 9a85af6b17766950feecd95d558906c6f54f3c8c
            new Object("paddle1", new Vector2(200, 200), new Vector2(200, 200), 40, 120, 0),
=======
            new Object("paddle1", new Vector2(200, 180), new Vector2(200, 180), 40, 120, 0),
            new Object("ball", new Vector2(0, 100), new Vector2(400, 350),50, 50, 3f),
>>>>>>> Fix rotation, add bounce func and add comments everywhere
        };
        private DynamicObject[] arrayDynamicObjectAll =
        {

        };

        private enum gameStates { MAINMENU, INGAME, QUIT };

        [STAThread]
        static void Main()
        {
            PongArena game = new PongArena();
            game.Run();
        }

        protected PongArena()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            gameState = gameStates.INGAME;
<<<<<<< 9a85af6b17766950feecd95d558906c6f54f3c8c
            listObjects.Add(arrayObjectAll[1]);
            listObjects.Add(arrayObjectAll[0]);
=======
            //adding Objects and Dynamic Objects to load
            listObjects.Add(arrayObjectAll[0]);
            listObjects.Add(arrayObjectAll[1]);
            listObjects.Add(arrayObjectAll[2]);
>>>>>>> Fix rotation, add bounce func and add comments everywhere
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case gameStates.MAINMENU:
                    break;
                case gameStates.INGAME:
                    GameStateInGame(gameTime);
                    break;
                case gameStates.QUIT:
                    Exit();
                    break;
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ///Loop through all DynamicObjects and set their Textures
            for (int i = 0; i < arrayDynamicObjectAll.Length; i++)
            {
                arrayDynamicObjectAll[i].setTexture(Content.Load<Texture2D>(arrayDynamicObjectAll[i].getName()));
            }
            ///loop through all Objects
            for (int i = 0; i < arrayObjectAll.Length; i++)
            {
                arrayObjectAll[i].setTexture(Content.Load<Texture2D>(arrayObjectAll[i].getName()));
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            ///Loop through DynamicObjects to draw sprites
            for (int i = 0; i < listDynamicObject.Count; i++)
            {
                spriteBatch.Draw(listDynamicObject[i].getTexture(), listDynamicObject[i].getLocation(), listDynamicObject[i].GetSourceRectangle(), Color.White);
            }
            ///loop through Objects to draw sprites
            for (int i = 0; i < listObjects.Count; i++)
            {
                Object x = listObjects[i];
                spriteBatch.Draw(x.getTexture(), x.getLocation(), x.getSourceRectangle(), Color.White, (float)x.getRotation(), x.getOrigin() , 1, SpriteEffects.None, 1);
            }
            spriteBatch.End();
        }

        /*************************************************************************************************************************************
        * GAME STATES
        ************************************************************************************************************************************/
        private void GameStateMainMenu(GameTime gameTime)
        {

        }

        private void GameStateInGame(GameTime gameTime)
        {
            //setup bouncetime testing
            int bounceInterval = 200;
            elapsedBounceTime += gameTime.ElapsedGameTime.Milliseconds;

            ///Loop through animList and check if enough time has passed to update to the next frame
            for (int i = 0; i < listDynamicObject.Count; i++)
            {
                listDynamicObject[i].Update(gameTime);
            }
            for(int i = 0; i < listObjects.Count; i++)
            {
                listObjects[i].Update(gameTime);
            }
<<<<<<< 9a85af6b17766950feecd95d558906c6f54f3c8c
            if(arrayObjectAll[0].CollidesWith(arrayObjectAll[1]))
            {
                arrayObjectAll[0].Bounce(arrayObjectAll[1]);
            }

            

=======
            ///check if Object is colliding with Object && enough time has passed to bounce again
            //A bounce interval is neccessary to prevent object from keeping bouncing as Object still collide just little after the bounce
            if(arrayObjectAll[0].CollidesWith(arrayObjectAll[1]) && elapsedBounceTime > bounceInterval)
            {
                arrayObjectAll[0].Bounce(arrayObjectAll[1]);
                elapsedBounceTime = 0;
            }
            
>>>>>>> Fix rotation, add bounce func and add comments everywhere
            //perform actions based on input
            InputHandler();
        }

        /*************************************************************************************************************************************
         * INPUT HANDLING
         ************************************************************************************************************************************/

        private void InputHandler()
        {
            KeyboardState state = Keyboard.GetState();
        }
    }
}