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

        public static SpriteBatch spriteBatch;
        Random r;

        Dictionary<string, ObjectToDrawBase> shapes = new Dictionary<string, ObjectToDrawBase>();

        public static int window_height;
        public static int window_width;
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

            shapes.Add("rect1", new Shape(graphics.GraphicsDevice, new Vector2(r.Next(0, window_width), r.Next(0, window_height)), 20, 80));

            Color[] data = new Color[shapes["rect1"].width * shapes["rect1"].height];
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

            for (int i = 0; i < shapes["rect1"].width; i++)
            {
                for (int j = 0; j < shapes["rect1"].height; j++)
                {
                    data[j * shapes["rect1"].width + i] = dataTemp[i, j];
                }
            }
            shapes["rect1"].setData(data);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fps_font = Content.Load<SpriteFont>("font/arial-36");
            title_font = Content.Load<SpriteFont>("font/title");

            Texture2D rect1Image = Content.Load<Texture2D>("img/thing");
            shapes.Add("rect2",  new Shape(rect1Image, new Vector2(r.Next(0, window_width), r.Next(0, window_height)), 80, 80));

            Texture2D coinImage = Content.Load<Texture2D>("img/images");
            shapes.Add("coin",  new AnimShape(coinImage, 1, 8, new Vector2(400,400)));
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
            if (state == GameState.GAMEPLAY_VIEW)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    shapes["rect1"].location.Y -=5;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    shapes["rect1"].location.Y += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    shapes["rect1"].location.X-=5;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    shapes["rect1"].location.X += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    shapes["rect1"].rotation += 0.1f;
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                    shapes["rect1"].rotation -= 0.1f;

                if (Keyboard.GetState().IsKeyDown(Keys.I))
                    shapes["rect2"].location.Y-=5;
                if (Keyboard.GetState().IsKeyDown(Keys.K))
                    shapes["rect2"].location.Y += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.J))
                    shapes["rect2"].location.X-=5;
                if (Keyboard.GetState().IsKeyDown(Keys.L))
                    shapes["rect2"].location.X += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                    shapes["rect2"].rotation += 0.1f;
                if (Keyboard.GetState().IsKeyDown(Keys.O))
                    shapes["rect2"].rotation -= 0.1f;

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    color_fit = true;
                }

                foreach (KeyValuePair<string,ObjectToDrawBase> shape in shapes)
                {
                    shape.Value.checkEdge();
                }

                Random r = new Random();
                
                if (((AnimShape)shapes["coin"]).checkEdgeCircle(shapes["rect1"].location.X, shapes["rect1"].location.Y))
                {
                    shapes["rect1"].location = new Vector2(r.Next(0, window_width), r.Next(0, window_height));
                }
                if (((AnimShape)shapes["coin"]).checkEdgeCircle(shapes["rect2"].location.X, shapes["rect2"].location.Y))
                {
                    shapes["rect2"].location = new Vector2(r.Next(0, window_width), r.Next(0, window_height));
                }
                
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    state = GameState.GAMEPLAY_VIEW;
                }
            }

            fps.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            shapes["coin"].Update();
            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (state == GameState.GAMEPLAY_VIEW)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();

                spriteBatch.DrawString(fps_font, string.Format("FPS: {0}", (int)fps.AverageFramesPerSecond), new Vector2(1, 1), Color.Black);


                foreach (KeyValuePair<string, ObjectToDrawBase> shape in shapes)
                {
                    shape.Value.Draw();
                }

                spriteBatch.End();

                base.Draw(gameTime);
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();

                spriteBatch.DrawString(title_font, "Title screen", new Vector2((window_width / 2) - title_font.MeasureString("Title screeen").X / 2, window_height / 2), Color.Black);

                spriteBatch.End();
            }
        }
    }

    public enum GameState
    {
        TITLESCREEN, GAMEPLAY_VIEW, GAMEPLAY_CODE,LEVEL_SELECT//todo add more states here
    }
}
