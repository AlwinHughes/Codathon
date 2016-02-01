using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Example_name
{
    class Thing
    {
        public Thing() { }

        public int x, y;
        public float rotation;
        public Texture2D texture;
        public Color[] color;
        public int width;
        public int height;

        public Thing(int x, int y,Texture2D texture,int array_x, int array_y,float rotation)
        {
            this.x = x;
            this.y = y;
            this.texture = texture; 
            color = new Color[array_x * array_y];
        }
      


    }
}
