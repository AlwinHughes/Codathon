
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Example_name
{
    class Shape
    {
        Texture2D texture;
        public int x;
        public int y;
        public int rotation;
        public Shape(int x, int y, int rotation)
        {
            this.x = x;
            this.y = y;
            this.rotation = rotation;
            this.texture = new Texture2D(Game1.graphics.GraphicsDevice, x, y);
        }

        public void draw()
        {
            Game1.spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
        }

        public void setData(Color[] data)
        {
            texture.SetData(data);
        }
    }
}
