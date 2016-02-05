using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
using System.Linq;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Example_name
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        FrameCounter fps = new FrameCounter();
        MouseDrag mouse = new MouseDrag();

        public static MouseState current;
        MouseState previous;

        SpriteFont fps_font;
        public static SpriteFont title_font;
        SpriteFont subtitle_font;
        

        public static GraphicsDeviceManager graphics;

        public static SpriteBatch spriteBatch;

        StorageDevice storageDevice;
        IAsyncResult asyncResult;
        PlayerIndex playerIndex = PlayerIndex.One;
        StorageContainer storageContainer;
        string filename = "savegame.sav";

        Random r;

        Dictionary<string, ObjectToDrawBase>[] shapes = new Dictionary<string, ObjectToDrawBase>[]
        { new Dictionary<string, ObjectToDrawBase>(), //Titlescreen
          new Dictionary<string, ObjectToDrawBase>(), //GamePlay_View
          new Dictionary<string, ObjectToDrawBase>(), //GamePlay_Code
          new Dictionary<string, ObjectToDrawBase>()}; //Level_Select

        public static int window_height;
        public static int window_width;
        bool color_fit = false;
        public static GameState state;

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
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            r = new Random();

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("rect1", new Shape(graphics.GraphicsDevice, new Vector2(r.Next(0, window_width), r.Next(0, window_height)), 20, 80));
            
            Color[] data = new Color[shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].width * shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].height];
            
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

            for (int i = 0; i < shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].width; i++)
            {
                for (int j = 0; j < shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].height; j++)
                {
                    data[j * shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].width + i] = dataTemp[i, j];
                }
            }
            shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].setData(data);
            base.Initialize();
        }
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fps_font = Content.Load<SpriteFont>("font/arial-36");
            subtitle_font = Content.Load<SpriteFont>("font/subtitle");
            title_font = Content.Load<SpriteFont>("font/title");
            //fonts.Add("fps_font",fps_font);
            //fonts.Add("subtitle_font", subtitle_font);
            //fonts.Add("title_font", title_font);

            Texture2D rect1Image = Content.Load<Texture2D>("img/thing");
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("rect2", new Shape(rect1Image, new Vector2(r.Next(0, window_width), r.Next(0, window_height)), 80, 80));

            Texture2D coinImage = Content.Load<Texture2D>("img/images");
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("coin", new AnimShape(coinImage, 1, 8, new Vector2(400, 400)));

            shapes[(int)GameState.TITLESCREEN].Add("subtitle", new TextShow(new Vector2((window_width / 2) , window_height / 2),0,Color.Transparent,Color.Transparent,subtitle_font,"Press Space",Color.Black,false));
            shapes[(int)GameState.TITLESCREEN]["subtitle"].center(new Vector2(0, 30)); 

            shapes[(int)GameState.TITLESCREEN].Add("title", new TextShow(new Vector2((window_width / 2), window_height / 2), 0, Color.Transparent, Color.Transparent, title_font, "Title Screen", Color.Black,false));
            shapes[(int)GameState.TITLESCREEN]["title"].center(new Vector2(0, -30));

            //things
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("start1", new TextShow(new Vector2(100, 200), blockType.START));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("stop1", new TextShow(new Vector2(200, 200), blockType.STOP));

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("f1", new TextShow(new Vector2(100, 300), blockType.MOVEFORWARD));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("f2", new TextShow(new Vector2(100, 300), blockType.MOVEFORWARD));

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("if1", new TextShow(new Vector2(100, 400), blockType.IF));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("if2", new TextShow(new Vector2(100, 400), blockType.IF));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("if3", new TextShow(new Vector2(100, 400), blockType.IF));

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("end1", new TextShow(new Vector2(100, 500), blockType.END));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("end2", new TextShow(new Vector2(100, 500), blockType.END));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("end3", new TextShow(new Vector2(100, 500), blockType.END));

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("for1", new TextShow(new Vector2(200, 400), blockType.FOR));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("end4", new TextShow(new Vector2(100, 500), blockType.END));

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("true1", new TextShow(new Vector2(100, 300), blockType.TRUE));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("true2", new TextShow(new Vector2(100, 300), blockType.TRUE));

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("false1", new TextShow(new Vector2(300, 400), blockType.FALSE));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("false2", new TextShow(new Vector2(300, 400), blockType.FALSE));

            shapes[(int)GameState.GAMEPLAY_VIEW].Add("not1", new TextShow(new Vector2(400, 400), blockType.NOT));
            shapes[(int)GameState.GAMEPLAY_VIEW].Add("not2", new TextShow(new Vector2(400, 400), blockType.NOT));
        }

        protected override void UnloadContent()
        {
            
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            current = Mouse.GetState();
            if (current.LeftButton == ButtonState.Pressed)
            {
                if (previous.LeftButton != ButtonState.Pressed)
                {
                    mouse.CheckClick(shapes[(int)state]);
                }
            }
            else
            {
                if (mouse.draggedObject != null)
                {
                    ((TextShow)mouse.draggedObject).Dock(shapes[(int)GameState.GAMEPLAY_VIEW]);
                    
                    mouse.draggedObject = null;
                }
            }
            mouse.Update();
            previous = current;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (state == GameState.GAMEPLAY_VIEW)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].location.Y -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].location.Y += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].location.X -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].location.X += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].rotation += 0.1f;
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].rotation -= 0.1f;

                if (Keyboard.GetState().IsKeyDown(Keys.I))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].location.Y -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.K))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].location.Y += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.J))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].location.X -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.L))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].location.X += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].rotation += 0.1f;
                if (Keyboard.GetState().IsKeyDown(Keys.O))
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].rotation -= 0.1f;

                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    saveGame();

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    color_fit = true;
                }

                foreach (KeyValuePair<string, ObjectToDrawBase> shape in shapes[(int)state])
                {
                    shape.Value.Update();
                }

                Random r = new Random();

                if (((AnimShape)shapes[(int)GameState.GAMEPLAY_VIEW]["coin"]).checkEdgeCircle(shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].location.X, shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].location.Y))
                {
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect1"].location = new Vector2(r.Next(0, window_width), r.Next(0, window_height));
                }
                if (((AnimShape)shapes[(int)GameState.GAMEPLAY_VIEW]["coin"]).checkEdgeCircle(shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].location.X, shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].location.Y))
                {
                    shapes[(int)GameState.GAMEPLAY_VIEW]["rect2"].location = new Vector2(r.Next(0, window_width), r.Next(0, window_height));
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
            shapes[(int)GameState.GAMEPLAY_VIEW]["coin"].Update();
            base.Update(gameTime);
        }

        public void saveGame()
        {
            SaveClass thing = new SaveClass();
            Dictionary<string, ObjectToDrawBase> temp = new Dictionary<string, ObjectToDrawBase>();
            foreach (KeyValuePair<string,ObjectToDrawBase> shape in shapes[(int)GameState.GAMEPLAY_VIEW])
            {
                if (shape.Value.is_text_show)
                {
                    temp.Add(shape.Key, shape.Value);
                }
            }

            foreach (KeyValuePair<string, ObjectToDrawBase> bob in temp)
            {
                Debug.WriteLine(bob.Key);
                thing.keys.Add(bob.Key);
                thing.values.Add(bob.Value);
            }

            
                using (Stream stream = File.Open("Save_data.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, thing);
                }
            
            

        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (state == GameState.TITLESCREEN)
            {
                bool sub_title_down = true;
                if (shapes[(int)GameState.TITLESCREEN]["subtitle"].location.Y <= 400)
                {
                    sub_title_down = false;
                }else if(shapes[(int)GameState.TITLESCREEN]["subtitle"].location.Y >= 450)
                {
                    sub_title_down = true;
                }
                if(sub_title_down)
                {
                    shapes[(int)GameState.TITLESCREEN]["subtitle"].location.Y -= 1f;
                }
                else
                {
                    
                    shapes[(int)GameState.TITLESCREEN]["subtitle"].location.Y += 1f;
                }
                


            }

            foreach (KeyValuePair<string, ObjectToDrawBase> shape in shapes[(int)state].Reverse())
            {
                shape.Value.Draw();
            }
            

            if (state == GameState.GAMEPLAY_VIEW)
            {
                spriteBatch.DrawString(fps_font, string.Format("FPS: ", Math.Ceiling(fps.AverageFramesPerSecond)), new Vector2(1, 1), Color.Black);
                
            }
            
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }

    public enum GameState
    {
        TITLESCREEN, GAMEPLAY_VIEW, GAMEPLAY_CODE, LEVEL_SELECT
    }

    [Serializable()]
    public class SaveClass
    {
        //public Dictionary<string, ObjectToDrawBase> data = new Dictionary<string, ObjectToDrawBase>();
        public List<string> keys = new List<string>();
        public List<ObjectToDrawBase> values = new List<ObjectToDrawBase>();

        public SaveClass() { }

    }
}
