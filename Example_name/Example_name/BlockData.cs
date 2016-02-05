using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_name
{
    enum blockType
    {
        START, STOP, IF, FOR, WHILE, END, NOT, TRUE, FALSE, data, MOVEFORWARD
    }

    class BlockData
    {
        blockType type;

        public string name { get; private set; }
        public bool[] canBeDockedTo { get; private set; }
        public Color insideColour { get; private set; }
        public Color[] borderColours { get; private set; }
        public Color textColour { get; private set; }

        public static string getName(blockType type)
        {
            BlockData t = new BlockData(type);
            return t.name;
        }

        public BlockData(blockType type)
        {
            this.type = type;
            switch (this.type)
            {
                case blockType.START:
                    name = "START";
                    canBeDockedTo = new bool[2] { true, false };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.STOP:
                    name = "STOP";
                    canBeDockedTo = new bool[2] { false, false };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.IF:
                    name = "IF";
                    canBeDockedTo = new bool[2] { true, true };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.FOR:
                    name = "FOR";
                    canBeDockedTo = new bool[2] { true, true };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green }
;                    textColour = Color.CadetBlue;
                    break;
                case blockType.WHILE:
                    name = "WHILE";
                    canBeDockedTo = new bool[2] { true, true };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.END:
                    name = "END";
                    canBeDockedTo = new bool[2] { true, false };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.NOT:
                    name = "NOT";
                    canBeDockedTo = new bool[2] { false, true };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.TRUE:
                    name = "True";
                    canBeDockedTo = new bool[2] { false, true };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.FALSE:
                    name = "False";
                    canBeDockedTo = new bool[2] { false, true };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
                case blockType.MOVEFORWARD:
                    name = "moveForward";
                    canBeDockedTo = new bool[2] { true, false };
                    insideColour = Color.Plum;
                    borderColours = new Color[4] { Color.BlanchedAlmond, Color.DarkCyan, Color.DeepPink, Color.Green };
                    textColour = Color.CadetBlue;
                    break;
            }
        }


    }
}
