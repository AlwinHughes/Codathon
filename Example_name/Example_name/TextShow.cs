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
    class TextShow : ObjectToDrawBase
    {
        string text;
        SpriteFont font;
        int border_size;
        Color inside;
        Color border;
        Color text_color;
        int sprite_length;
        int sprite_height;

        public TextShow(Vector2 location, int border_size, Color inside, Color border, SpriteFont font, string text, Color text_color)
            : base(location, (int)font.MeasureString(text).X + 8 + border_size, (int)font.MeasureString(text).Y + 8 + border_size)
        {
            this.font = font;
            this.inside = inside;
            this.border = border;
            this.border_size = border_size;
            this.text_color = text_color;
            this.text = text;
            sprite_height = (int)font.MeasureString(text).Y;
            sprite_length = (int)font.MeasureString(text).X;

            Color[] data = new Color[width * height];
            Color[,] data_to_convert = new Color[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i < border_size || i > width - border_size || j < border_size || j > height - border_size)
                    {
                        data_to_convert[i, j] = border;
                    }
                    else
                    {
                        data_to_convert[i, j] = inside;
                    }
                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    data[j * width + i] = data_to_convert[i, j];
                }
            }
            texture = new Texture2D(Game1.graphics.GraphicsDevice, width, height);
            setData(data);
        }

        public override void Draw()
        {
            Game1.spriteBatch.Draw(texture, new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height), Color.White);
            Game1.spriteBatch.DrawString(font, text, new Vector2(location.X + border_size , location.Y + border_size), text_color);
        }
    }
}
