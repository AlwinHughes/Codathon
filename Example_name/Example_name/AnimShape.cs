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
    class AnimShape : ObjectToDrawBase
    {
        private int currentFrame;
        private int totalFrames;
        private int cutWidth;
        private int cutHeight;
        private int row;
        private int column;

        public AnimShape(Texture2D texture, int width, int height, Vector2 location)
            :base(texture,location,width,height)
        {
            currentFrame = 0;
            totalFrames = this.width * this.height;
        }

        public override void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        override public void Draw()
        {
            cutWidth = texture.Width / height;
            cutHeight = texture.Height / width;
            row = currentFrame / height;
            column = currentFrame % height;

            Rectangle sourceRectangle = new Rectangle(cutWidth * column, cutHeight * row, cutWidth, cutHeight);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, cutWidth, cutHeight);

            Game1.spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public bool checkEdgeCircle(float x, float y)
        {
            if (Math.Pow(x - location.X, 2) + Math.Pow(y - location.Y - height / 2, 2) < Math.Pow(cutWidth,2))
            {
                Debug.WriteLine("collided");
                return true;
            }
            return false;
        }
    }
}
