using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Example_name
{
    class MouseDrag
    {
        public ObjectToDrawBase draggedObject;
        Vector2 offset;

        public void CheckClick(Dictionary<string, ObjectToDrawBase> shapes)
        {
            foreach (KeyValuePair<string, ObjectToDrawBase> pair in shapes)
            {
                ObjectToDrawBase shape = pair.Value;
                if (shape.canBeDocked)
                {
                    if (Game1.current.X > shape.location.X && Game1.current.X < shape.location.X + shape.width && Game1.current.Y > shape.location.Y && Game1.current.Y < shape.location.Y + shape.height)
                    {
                        draggedObject = shape;
                        draggedObject.dock = null;
                        offset = new Vector2(Game1.current.X - shape.location.X, Game1.current.Y - shape.location.Y);
                        //why alwin why shapes.Remove(pair.Key);
                        //shapes.Add(pair.Key, pair.Value);
                        return;
                    }
                }
            }
        }

        public void Update()
        {
            if (draggedObject != null)
            {
                draggedObject.location = Game1.current.Position.ToVector2() - offset;
            }
        }

    }
}
