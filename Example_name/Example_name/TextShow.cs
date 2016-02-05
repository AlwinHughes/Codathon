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
    [Serializable]
    class TextShow : ObjectToDrawBase
    {
        string text;
        SpriteFont font;

        int border_size;
        Color inside_color;
        Color border_color;
        Color text_color;

        int sprite_length;
        int sprite_height;

        Color[] data;
        Color[,] data_to_convert;

        Color[] border_colors;
        int[] border_widths;

        bool complex;



        
        

        public BlockData blockData;


        // simple creation
        public TextShow(Vector2 location, int border_size, Color inside_color, Color border_color, SpriteFont font, string text, Color text_color, bool can_be_draged)
            : base(location, (int)font.MeasureString(text).X + 8 + border_size, (int)font.MeasureString(text).Y + 8 + border_size)
        {
            is_text_show = true;
            complex = false;
            this.font = font;
            this.inside_color = inside_color;
            this.border_color = border_color;
            this.border_size = border_size;
            this.text_color = text_color;
            this.text = text;
            this.canBeDocked = can_be_draged;
            sprite_height = (int)font.MeasureString(text).Y;
            sprite_length = (int)font.MeasureString(text).X;

            data = new Color[width * height];
            data_to_convert = new Color[width, height];

            generateTexture(border_size, inside_color, border_color, text_color);

        }

        //constructur used for complex creation
        public TextShow(Vector2 location, Color inside_color, Color[] border_colors, int[] border_widths, SpriteFont font, string text, Color text_color, bool can_be_draged)
            : base(location, (int)font.MeasureString(text).X + 8 + border_widths[0] + border_widths[2], (int)font.MeasureString(text).Y + 8 + border_widths[1] + border_widths[3])

        {
            is_text_show = true;
            complex = true;
            this.font = font;
            this.inside_color = inside_color;
            this.border_colors = border_colors;
            this.text_color = text_color;
            this.text = text;
            this.canBeDocked = can_be_draged;
            this.border_widths = border_widths;
            sprite_height = (int)font.MeasureString(text).Y;
            sprite_length = (int)font.MeasureString(text).X;

            data = new Color[width * height];
            data_to_convert = new Color[width, height];

            generateTextureComplex(border_widths, border_colors, inside_color);


        }

        public void generateTextureComplex(int[] boder_sizes, Color[] border_colors, Color inside)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i <= boder_sizes[1])
                    {
                        data_to_convert[i, j] = border_colors[0];
                    }
                    else if (j <= boder_sizes[1])
                    {
                        data_to_convert[i, j] = border_colors[1];
                    }
                    else if (j >= height - boder_sizes[3])
                    {
                        data_to_convert[i, j] = border_colors[3];
                    }
                    else if (i >= width - boder_sizes[2])
                    {

                        data_to_convert[i, j] = border_colors[2];
                    }
                    else
                    {
                        data_to_convert[i, j] = inside;
                    }
                }
            }
            convertTo1DArray();
        }
        public void generateTexture(int border_size, Color inside, Color border, Color text_color)//only alows color change not size change 
        {
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
            convertTo1DArray();

        }

        private void convertTo1DArray()
        {
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
                if (shape.Value is TextShow && ((TextShow)shape.Value).blockData != null)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        TextShow s = (TextShow)shape.Value;
                        Vector2[] offsets = new Vector2[2] {new Vector2(0, s.height), new Vector2(s.width, 0)};

                        if (s.blockData.canBeDockedTo[i] && this != s && Game1.current.X - offsets[i].X > s.location.X && Game1.current.X - offsets[i].X < s.location.X + width && Game1.current.Y - offsets[i].Y > s.location.Y && Game1.current.Y - offsets[i].Y < s.location.Y + height)
                        {
                            dock = shape.Value;
                            dockOffset = offsets[i];
                            return;
                        }
                    }
                }
            }
        }
    }
}
