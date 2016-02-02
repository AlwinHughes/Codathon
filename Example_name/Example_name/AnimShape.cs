using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Example_name
{
    class AnimShape
    {
        Texture2D texture;
        public Vector2 location;

        public int rows;
        public int columns;

        private int currentFrame;
        private int totalFrames;

        public AnimShape(Texture2D texture, int rows, int columns, Vector2 location)
        {
            this.location = location;
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            currentFrame = 0;
            totalFrames = this.rows * this.columns;
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw()
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            Game1.spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void checkEdge()
        {
            if (location.X + texture.Width > Game1.window_width)
            {
                location.X = Game1.window_width;
            }
            if (location.X < 0)
            {
                location.X = 0;
            }
            if (location.Y + texture.Height > Game1.window_height)
            {
                location.Y = Game1.window_height - texture.Height;
            }
            if (location.Y < 0)
            {
                location.Y = 0;
            }
        }
        public bool checkEdgeCircle(float x, float y)
        {
            if(Math.Pow(x-location.X,2) + Math.Pow(y-location.Y,2)< texture.Width)
            {
                Debug.WriteLine("collided");
                return true;
            }
            return false;
        }
    }
}
