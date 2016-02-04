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

        public BlockData(blockType type)
        {
            this.type = type;
            switch (this.type)
            {
                case blockType.START:
                    name = "START";
                    canBeDockedTo = new bool[2] { true, false };
                    break;
                case blockType.STOP:
                    name = "STOP";
                    canBeDockedTo = new bool[2] { false, false };
                    break;
                case blockType.IF:
                    name = "IF";
                    canBeDockedTo = new bool[2] { true, true };
                    break;
                case blockType.FOR:
                    name = "FOR";
                    canBeDockedTo = new bool[2] { true, true };
                    break;
                case blockType.WHILE:
                    name = "WHILE";
                    canBeDockedTo = new bool[2] { true, true };
                    break;
                case blockType.END:
                    name = "END";
                    canBeDockedTo = new bool[2] { true, false };
                    break;
                case blockType.NOT:
                    name = "NOT";
                    canBeDockedTo = new bool[2] { false, true };
                    break;
                case blockType.TRUE:
                    name = "True";
                    canBeDockedTo = new bool[2] { false, true };
                    break;
                case blockType.FALSE:
                    name = "False";
                    canBeDockedTo = new bool[2] { false, true };
                    break;
                case blockType.MOVEFORWARD:
                    name = "moveForward";
                    canBeDockedTo = new bool[2] { true, false };
                    break;
            }
        }


    }
}
