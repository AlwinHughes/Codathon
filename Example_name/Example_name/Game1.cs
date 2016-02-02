using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace Example_name
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        FrameCounter fps = new FrameCounter();

        SpriteFont font;
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        Shape rect1;
        Shape rect2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Random random = new Random();
            rect1 = new Shape(random.Next(0,500), random.Next(0, 500), 80, 80);//todo makes this generate random number based on screen size
            rect2 = new Shape(random.Next(0, 500), random.Next(0, 500), 20, 80);

            Color[] data = new Color[rect1.getWidth() * rect1.getHeight()];
            
            for (int i = 0; i < data.Length; i++)
            {
                if (i % 13 == 0 || i % 2 == 0)
                {
                    data[i] = Color.Red;
                }
                else
                {
                    data[i] = Color.Green;
                }
            }
            rect1.setData(data);

            data = new Color[rect2.getWidth() * rect2.getHeight()];
            Color[,] dataTemp = new Color[20, 80];

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    if (i < 4 || i > 15 || j < 4 || j > 75)
                    {
                        dataTemp[i, j] = Color.White;
                    }
                    else
                    {
                        dataTemp[i, j] = Color.Blue;
                    }
                }
            }

            for (int i = 0; i < rect2.getWidth(); i++)
            {
                for (int j = 0; j < rect2.getHeight(); j++)
                {
                    data[j * rect2.getWidth() + i] = dataTemp[i, j];
                }
            }
            rect2.setData(data);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font/arial-36");
            //use this.Content to load your game content here
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                rect1.y--;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                rect1.y++;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                rect1.x--;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                rect1.x++;
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                rect1.rotation--;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                rect1.rotation++;

            if (Keyboard.GetState().IsKeyDown(Keys.I))
                rect2.y--;
            if (Keyboard.GetState().IsKeyDown(Keys.K))
                rect2.y++;
            if (Keyboard.GetState().IsKeyDown(Keys.J))
                rect2.x--;
            if (Keyboard.GetState().IsKeyDown(Keys.L))
                rect2.x++;
            if (Keyboard.GetState().IsKeyDown(Keys.U))
                rect2.rotation--;
            if (Keyboard.GetState().IsKeyDown(Keys.O))
                rect2.rotation++;

            base.Update(gameTime);
        }
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            fps.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            string s = string.Format("FPS: {0}",(int)fps.AverageFramesPerSecond);

            spriteBatch.DrawString(font, s, new Vector2(1, 1), Color.Black);
            Debug.WriteLine(s);
            
            rect1.draw();
            rect2.draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
