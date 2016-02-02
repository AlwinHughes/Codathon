using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace Example_name
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        FrameCounter fps = new FrameCounter();

        SpriteFont fps_font;
        SpriteFont title_font;

        public static GraphicsDeviceManager graphics;

        SpriteFont font;
        
        public static SpriteBatch spriteBatch;
        Random r;

        Shape rect1;

        Shape rect2;

        AnimShape coin;

        int window_height;
        int window_width;
        bool color_fit = false;
        GameState state;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            state = GameState.TITLESCREEN;
            window_height = graphics.GraphicsDevice.DisplayMode.Height;
            window_width = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = window_height;
            graphics.PreferredBackBufferWidth = window_width;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            r = new Random();
            rect2 = new Shape(graphics.GraphicsDevice, r.Next(0, window_width), r.Next(0, window_height), 20, 80);

            Color[] data = new Color[rect2.getWidth() * rect2.getHeight()];
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

            fps_font = Content.Load<SpriteFont>("font/arial-36");
            title_font = Content.Load<SpriteFont>("font/title");

            font = Content.Load<SpriteFont>("font/arial-36");

            Texture2D rect1Image = Content.Load<Texture2D>("img/thing");
            rect1 = new Shape(rect1Image, r.Next(0, window_width), r.Next(0, window_height), 80, 80);

            Texture2D coinImage = Content.Load<Texture2D>("img/images");
            coin = new AnimShape(coinImage, 1,8,new Vector2(100,100));

            Texture2D image = Content.Load<Texture2D>("img/thing");
            rect1 = new Shape(image, r.Next(0, window_width), r.Next(0, window_height), 80, 80);


        }

        protected override void UnloadContent()
        {

        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (state == GameState.GAMEPLAY)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    rect1.location.Y--;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    rect1.location.Y++;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    rect1.location.X--;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    rect1.location.X++;
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    rect1.rotation = rect1.rotation + 0.1f;
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                    rect1.rotation = rect1.rotation - 0.1f;

                if (Keyboard.GetState().IsKeyDown(Keys.I))
                    rect2.location.Y--;
                if (Keyboard.GetState().IsKeyDown(Keys.K))
                    rect2.location.Y++;
                if (Keyboard.GetState().IsKeyDown(Keys.J))
                    rect2.location.X--;
                if (Keyboard.GetState().IsKeyDown(Keys.L))
                    rect2.location.X++;
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                    rect2.rotation = rect2.rotation - 0.1f;
                if (Keyboard.GetState().IsKeyDown(Keys.O))
                    rect2.rotation = rect2.rotation + 0.1f;

                if (rect2.location.X > window_width || rect2.location.X < 0)
                {
                    rect2.location.X = window_width / 2;
                }
                if (rect2.location.Y > window_height || rect2.location.Y < 0)
                {
                    rect2.location.Y = window_height / 2;
                }

                if (rect1.location.X > window_width || rect1.location.X < 0)
                {
                    rect1.location.X = window_width / 2;
                }
                if (rect1.location.Y > window_height || rect1.location.Y < 0)
                {
                    rect1.location.Y = window_height / 2;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    color_fit = true;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    state = GameState.GAMEPLAY;
                }
            }

            fps.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            coin.Update();
            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (state == GameState.GAMEPLAY)
            {

                
                GraphicsDevice.Clear(Color.CornflowerBlue);
                

                GraphicsDevice.Clear(Color.CornflowerBlue);


                spriteBatch.Begin();

                //fps.Update((float)gameTime.ElapsedGameTime.TotalSeconds);


                spriteBatch.DrawString(fps_font, string.Format("FPS: {0}", (int)fps.AverageFramesPerSecond) , new Vector2(1, 1), Color.Black);
               

               

                rect1.Draw();
                rect2.Draw();
                coin.Draw();
                spriteBatch.End();

                base.Draw(gameTime);
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();

                spriteBatch.DrawString(title_font, "Title screen", new Vector2((window_width / 2)-  title_font.MeasureString("Title screeen").X, window_height / 2), Color.Black);

                spriteBatch.End();
            }
        }
    }

    public enum GameState
    {
        GAMEPLAY, TITLESCREEN//todo add more states here
    }
}
