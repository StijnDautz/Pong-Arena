using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace circlePong
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D character1; //Textures
        Texture2D character2;
        Texture2D ball;

       

        int circleRadius = 400; //Diameter mainCircle
        int circleXPos;
        int circleYPos;
        int turn; //Who's turn it currently is

        double tPosCharacter1 = 0; //Character1
        double tSpeedCharacter1 = 0.05;
        double xPosCharacter1;
        double yPosCharacter1;
        int character1Length = 100;
        int character1Points = 0;
        

        double tPosCharacter2 = 0; //Character2
        double tSpeedCharacter2 = 0.05;
        double xPosCharacter2;
        double yPosCharacter2;
        int character2Length = 100;
        int character2Points = 0;

        double xBall = 900, yBall = 600; // Ball
        double xSpeedBall = 2;
        double ySpeedBall = 2;
        double directionBall;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            
            this.IsMouseVisible = true;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            character1 = new Texture2D(graphics.GraphicsDevice, character1Length, 5);
            setColor(character1, Color.Blue);

            character2 = new Texture2D(graphics.GraphicsDevice, character2Length, 5);
            setColor(character2, Color.Red);

            ball = new Texture2D(graphics.GraphicsDevice, 5, 5);
            setColor(ball, Color.Yellow);

            circleXPos = graphics.PreferredBackBufferWidth / 2;
            circleYPos = graphics.PreferredBackBufferHeight / 2;
            
            base.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        public void setColor(Texture2D texture, Color color)
        {
            Color[] data = new Color[texture.Width * texture.Height];
            for (int i = 0; i < data.Length; i++) 
            {
                data[i] = color;
            }
            texture.SetData(data);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.A)) tPosCharacter1 += tSpeedCharacter1;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) tPosCharacter1 -= tSpeedCharacter1;

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) tPosCharacter2 += tSpeedCharacter2;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) tPosCharacter2 -= tSpeedCharacter2;
            // TODO: Add your update logic here

            //------------------------------------------------
            //Character1
            xPosCharacter1 = circleRadius*System.Math.Cos(tPosCharacter1) + circleXPos;
            yPosCharacter1 = circleRadius*System.Math.Sin(tPosCharacter1) + circleYPos;
            
            //Character2
            xPosCharacter2 = circleRadius * System.Math.Cos(tPosCharacter2) + circleXPos;
            yPosCharacter2 = circleRadius * System.Math.Sin(tPosCharacter2) +  circleYPos;

            //Ball
            if (System.Math.Sqrt((xBall - circleXPos) * (xBall - circleXPos) + (yBall - circleYPos) * (yBall - circleYPos)) > circleRadius) //if d(ball, center of circle) > radius ...
            {
                if (System.Math.Tanh((xBall - circleXPos) / (yBall - circleYPos)) > System.Math.Tanh((xPosCharacter1 - circleXPos) / (yPosCharacter1 - circleYPos)) && System.Math.Tanh((xBall - circleXPos) / (yBall - circleYPos)) < System.Math.Tanh((xPosCharacter1 - circleXPos) / (yPosCharacter1 - circleYPos)) + (2*System.Math.PI * (character1Length/(System.Math.PI * 2 * circleRadius))))
                {
                    double newAngle = (tPosCharacter1 + System.Math.PI * 0.5) + 2 * (tPosCharacter1 + System.Math.PI * 0.5 - directionBall) + System.Math.PI;
                    double currentSpeed = System.Math.Sqrt(xSpeedBall * xSpeedBall + ySpeedBall * ySpeedBall);
                    xBall -= 4 * (int)xSpeedBall; //resetten ball
                    yBall -= 4 * (int)ySpeedBall;

                    xSpeedBall = System.Math.Cos(System.Math.PI * 0.5 + newAngle) * currentSpeed; //0.5 pi maakt hier niet uit, right?
                    ySpeedBall = System.Math.Sin(System.Math.PI * 0.5 + newAngle) * currentSpeed;
                    turn = 2;
                }
                if (System.Math.Tanh((xBall - circleXPos) / (yBall - circleYPos)) > System.Math.Tanh((xPosCharacter2 - circleXPos) / (yPosCharacter2 - circleYPos)) && System.Math.Tanh((xBall - circleXPos) / (yBall - circleYPos)) < System.Math.Tanh((xPosCharacter2 - circleXPos) / (yPosCharacter2 - circleYPos)) + (2 * System.Math.PI * (character2Length / (System.Math.PI * 2 * circleRadius))))
                {
                    double newAngle = (tPosCharacter2 + System.Math.PI * 0.5) + 2 * (tPosCharacter2 + System.Math.PI * 0.5 - directionBall) + System.Math.PI;
                    double currentSpeed = System.Math.Sqrt(xSpeedBall * xSpeedBall + ySpeedBall * ySpeedBall);
                    xBall -= 4 * (int)xSpeedBall; //resetten ball
                    yBall -= 4 * (int)ySpeedBall;

                    xSpeedBall = System.Math.Cos(System.Math.PI * 0.5 + newAngle) * currentSpeed; //0.5 pi maakt hier niet uit, right?
                    ySpeedBall = System.Math.Sin(System.Math.PI * 0.5 + newAngle) * currentSpeed;
                    turn = 1;
                }
                if (System.Math.Sqrt((xBall - circleXPos) * (xBall - circleXPos) + (yBall - circleYPos) * (yBall - circleYPos)) > circleRadius + 20)
                {
                    switch (turn)
                    {
                        case 1: {
                                character2Points++;
                                xBall = circleXPos;
                                yBall = circleYPos;
                                System.Diagnostics.Debug.WriteLine("Player2 has " + character2Points + " points!");
                                break;
                                }
                        case 2:
                                {
                                character1Points++;
                                xBall = circleXPos;
                                yBall = circleYPos;
                                System.Diagnostics.Debug.WriteLine("Player1 has " + character1Points + " points!");
                                break;
                                }
                        default:{
                                break;
                                }

                            }
                    }
                
            }
            
            xBall += (int)xSpeedBall;
            yBall += (int)ySpeedBall;
            directionBall = System.Math.Tanh(ySpeedBall / xSpeedBall);
 
            //------------------------------------------------
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(character1, new Vector2((int)xPosCharacter1, (int)yPosCharacter1), null, Color.White * 0.6f, (float)(tPosCharacter1 + 0.5 * System.Math.PI), new Vector2(character1.Width, character1.Height),1, SpriteEffects.None, 0); //Character1
            spriteBatch.Draw(character2, new Vector2((int)xPosCharacter2, (int)yPosCharacter2), null, Color.White * 0.6f, (float)(tPosCharacter2 + 0.5 * System.Math.PI), new Vector2(character2.Width, character2.Height), 1, SpriteEffects.None, 0); //Character2
            spriteBatch.Draw(ball, new Vector2((int)xBall, (int)yBall), Color.White); //Ball
            spriteBatch.End();
        }
    }
}
