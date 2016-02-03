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
    class AnimShape : ObjectToDrawBase
    {
        private int currentFrame;
        private int totalFrames;

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
            int cutWidth = texture.Width / height;
            int cutHeight = texture.Height / width;
            int row = currentFrame / height;
            int column = currentFrame % height;

            Rectangle sourceRectangle = new Rectangle(cutWidth * column, cutHeight * row, cutWidth, cutHeight);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, cutWidth, cutHeight);

            Game1.spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        override public void checkEdge()
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
            if(Math.Pow(x-location.X-width/2,2) + Math.Pow(y-location.Y-height/2,2)< Math.Pow(texture.Width-location.X,2))
            {
                Debug.WriteLine("collided");
                return true;
            }
            return false;
        }
      
    }
}
