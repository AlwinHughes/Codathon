using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Example_name
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
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

            rect1 = new Shape(80, 80, 0);
            rect2 = new Shape(20, 80, 0);

            Color[] data = new Color[80 * 80];
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

            data = new Color[20 * 80];
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
            int width = dataTemp.GetLength(0);
            int height = dataTemp.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    data[j * width + i] = dataTemp[i, j];
                }
            }
            rect2.setData(data);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            rect1.draw();
            rect2.draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
