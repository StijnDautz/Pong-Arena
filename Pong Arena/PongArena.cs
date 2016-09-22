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

        //Initialize
        private Object[] arrayObjectAll =
        {

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
            ///Loop through Objects to draw sprites
            for (int i = 0; i < listDynamicObject.Count; i++)
            {
                spriteBatch.Draw(listDynamicObject[i].getTexture(), listDynamicObject[i].getLocation(), listDynamicObject[i].GetSourceRectangle(), Color.White);
            }

            for (int i = 0; i < listObjects.Count; i++)
            {
                Object x = listObjects[i];
                spriteBatch.Draw(x.getTexture(), x.getLocation(), x.getSourceRectangle(), Color.White, x.getRotation(), Vector2.Zero, 1, SpriteEffects.None, 1);
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
            ///Loop through animList and check if enough time has passed to update to the next frame
            for (int i = 0; i < listDynamicObject.Count; i++)
            {
                listDynamicObject[i].Update(gameTime);
            }

            //perform actions based on input
            InputHandler();
        }

        /*************************************************************************************************************************************
         * INPUT HANDLING
         ************************************************************************************************************************************/

        private void InputHandler()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D))
            {
                arrayObjectAll[1].Move(new Vector2(5, 0));
            }
            if (state.IsKeyDown(Keys.A))
            {
                arrayObjectAll[1].Move(new Vector2(-5, 0));
            }
            if (state.IsKeyDown(Keys.W))
            {
                arrayObjectAll[1].Move(new Vector2(0, -5));
            }
            if (state.IsKeyDown(Keys.S))
            {
                arrayObjectAll[1].Move(new Vector2(0, 5));
            }
        }
    }
}