
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
        public float rotation;

        int width;
        int height;

        public Shape(Texture2D texture,int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.texture = texture;
            this.width = width;
            this.height = height;
        }

        public Shape(GraphicsDevice d, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            texture = new Texture2D(d, width, height);
            this.width = width;
            this.height = height;
        }

        public void draw()
        {
            if (rotation != 0)
            {
                Game1.spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
            }
            else
            {
                Game1.spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.White);
            }
        }

        public void setData(Color[] data)
        {
            texture.SetData(data);
        }

        public int getHeight()
        {
            return height;
        }

        public int getWidth()
        {
            return width;
        }
    }
}
