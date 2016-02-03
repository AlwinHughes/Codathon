
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
    }
}
