
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Example_name
{
    class Shape:ObjectToDrawBase
    {
        

        public Shape(Texture2D texture, Vector2 location, int width, int height)
            :base(texture,location,width,height)
        {
            
        }

        public Shape(GraphicsDevice d, Vector2 location, int width, int height)
            :base( d,  location, width, height)
        {
            
        }

        

       

        public void checkEdge()
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
    }
}
