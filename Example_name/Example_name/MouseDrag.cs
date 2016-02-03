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

        public void CheckClick(Dictionary<string, ObjectToDrawBase> shapes)
        {
            foreach (KeyValuePair<string, ObjectToDrawBase> pair in shapes)
            {
                ObjectToDrawBase shape = pair.Value;
                if (shape.can_be_draged)
                {
                    if (Game1.current.X > shape.location.X && Game1.current.X < shape.location.X + shape.width)
                    {
                        if (Game1.current.Y > shape.location.Y && Game1.current.Y < shape.location.Y + shape.height)
                        {
                            draggedObject = shape;
                            return;
                        }
                    }
                }
            }
        }

        public void Update()
        {
            if (draggedObject != null)
            {
                draggedObject.location = Game1.current.Position.ToVector2();
            }
        }

    }
}
