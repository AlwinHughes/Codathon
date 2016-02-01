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
        SpriteBatch spriteBatch;
        int x = 6;
        int y = 7;
        float rotation = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                y--;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                y++;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                x--;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                x++;
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                rotation--;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                rotation++;

                    // TODO: Add your update logic here

                    base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
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

            Color[] data_1 = new Color[20 * 80];

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
            
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++)
                {
                    Color thing = data_to_convert[i, j];
                    //Debug.WriteLine(thing);
                    data_1[j*width + i] = thing;
                }
            }

            

            rect_1.SetData(data_1);
            rect_2.SetData(data_2);
            Vector2 coor_2 = new Vector2(500, 500);
            Vector2 coor_1 = new Vector2(20, 20);
            var origin = new Vector2(rect_1.Width / 2, rect_1.Height / 2);
            spriteBatch.Draw(rect_1, new Rectangle(x, y, rect_1.Width, rect_1.Height),null, Color.White, rotation, origin, SpriteEffects.None, 0f);
            spriteBatch.Draw(rect_2, new Rectangle(x+40, y-40, rect_2.Width, rect_2.Height), null, Color.White, rotation, origin, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);



        }
               
            //
            //data[i] = Color.White;
        

            

        }
        }
 