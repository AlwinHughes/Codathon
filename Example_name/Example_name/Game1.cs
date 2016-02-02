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
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        Shape rect1;
        Shape rect2;
        int x = 6;
        int y = 7;
        float rotation = 0;
        Color[] data_1;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            rect1 = new Shape(40, 40, 0, new Texture2D(graphics.GraphicsDevice, 40, 40));
            rect2 = new Shape(40, 40, 0, new Texture2D(graphics.GraphicsDevice, 40, 40));

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
//<<<<<<< HEAD
            Debug.WriteLine("load content called");
            // Create a new SpriteBatch, which can be used to draw textures.
//=======
//>>>>>>> 25d3d960de5b4ce411105b6368e1a389d61c4ef4
            spriteBatch = new SpriteBatch(GraphicsDevice);
            data_1 = new Color[20 * 80];

            Color[,] data_to_convert = new Color[20, 80];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    if (i < 4 || i > 15 || j < 4 || j > 75)
                    {
                        data_to_convert[i, j] = Color.White;
                        //Debug.WriteLine("white");
                    }
                    else {
                        data_to_convert[i, j] = Color.Blue;
                        //Debug.WriteLine("blue");
                    }

                }
            }

            int width = data_to_convert.GetLength(0);
            int height = data_to_convert.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color thing = data_to_convert[i, j];
                    //Debug.WriteLine(thing);
                    data_1[j * width + i] = thing;
                }
            }

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
//<<<<<<< HEAD
                rotation = rotation+ 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                rotation = rotation -0.1f;

                    // TODO: Add your update logic here

                    
//=======
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
//>>>>>>> 25d3d960de5b4ce411105b6368e1a389d61c4ef4
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
//<<<<<<< HEAD
            Texture2D rect_1 = new Texture2D(graphics.GraphicsDevice, 20, 80);
            Texture2D rect_2 = new Texture2D(graphics.GraphicsDevice, 40, 80);
            //Texture2D other_rect = new Texture2D(graphics.GraphicsDevice);


            Color[] data_2 = new Color[80 * 80];
            for (int i = 0; i < data_2.Length; i++)
            {
                if (i % 13 == 0 || i % 2 == 0)
                {
                    data_2[i] = Color.Red;
                }
            }
            

            

            rect_1.SetData(data_1);
            rect_2.SetData(data_2);
            Vector2 coor_2 = new Vector2(500, 500);
            Vector2 coor_1 = new Vector2(20, 20);
            var origin = new Vector2(rect_1.Width / 2, rect_1.Height / 2);
            spriteBatch.Draw(rect_1, new Rectangle(x, y, rect_1.Width, rect_1.Height),null, Color.White, rotation, origin, SpriteEffects.None, 0f);
            spriteBatch.Draw(rect_2, new Rectangle(x+40, y-40, rect_2.Width, rect_2.Height), null, Color.White, rotation, origin, SpriteEffects.None, 0f);
//=======
            rect1.draw();
            rect2.draw();
//>>>>>>> 25d3d960de5b4ce411105b6368e1a389d61c4ef4
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

   
}
