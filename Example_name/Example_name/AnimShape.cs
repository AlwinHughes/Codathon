using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
