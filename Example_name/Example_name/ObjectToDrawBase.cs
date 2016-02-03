﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Example_name
{
    abstract class ObjectToDrawBase
    {
        public Texture2D texture;
        public Vector2 location;
        public float rotation;
        public int width { get; private set; }
        public int height { get; private set; }

        public ObjectToDrawBase(Texture2D texture, Vector2 location, int width, int height)
        {
            this.location = location;
            this.texture = texture;
            this.width = width;
            this.height = height;
        }

        public ObjectToDrawBase(GraphicsDevice d, Vector2 location, int width, int height)
        {
            this.location = location;
            texture = new Texture2D(d, width, height);
            this.width = width;
            this.height = height;
        }

        virtual public void Draw()
        {
            if (rotation != 0)
            {
                Game1.spriteBatch.Draw(texture, new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height), null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
            }
            else
            {
                Game1.spriteBatch.Draw(texture, new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height), Color.White);
            }
        }

        virtual public void Update()
        {
            //eh eh
        }

        virtual public void checkEdge()
        {
            if (location.X + width > Game1.window_width)
            {
                location.X = Game1.window_width;
            }
            if (location.X < 0)
            {
                location.X = 0;
            }
            if (location.Y + height > Game1.window_height)
            {
                location.Y = Game1.window_height - height;
            }
            if (location.Y < 0)
            {
                location.Y = 0;
            }
        }

        public void setData(Color[] data)
        {
            texture.SetData(data);
        }
    }
}
