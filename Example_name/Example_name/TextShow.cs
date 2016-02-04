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

        public TextShow(Vector2 location, int border_size, Color inside, Color border, SpriteFont font, string text, Color text_color, bool can_be_draged)
            : base(location, (int)font.MeasureString(text).X + 8 + border_size, (int)font.MeasureString(text).Y + 8 + border_size)
        {
            this.font = font;
            this.inside = inside;
            this.border = border;
            this.border_size = border_size;
            this.text_color = text_color;
            this.text = text;
            this.canBeDocked = can_be_draged;
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
            Game1.spriteBatch.DrawString(font, text, new Vector2(location.X + border_size, location.Y + border_size), text_color);
        }

        public Vector2 getOffset()
        {
            return new Vector2(4 + border_size + sprite_length / 2, 4 + border_size + sprite_height / 2);
        }

        public void Dock(Dictionary<string, ObjectToDrawBase> shapes)
        {
            foreach (KeyValuePair<string, ObjectToDrawBase> shape in shapes)
            {
                if (shape.Value.canBeDockedTo[2] && this != shape.Value)//avoid self only down dock temp
                {
                    dock = shape.Value;//temp
                    return;
                }
            }
        }
        public void asignDocking(bool up,bool right,bool down,bool left)
        {
            canBeDockedTo[0] = up;
            canBeDockedTo[1] = right;
            canBeDockedTo[2] = down;
            canBeDockedTo[3] = left;
        }

    }
}
