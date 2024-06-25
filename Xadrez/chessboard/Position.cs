
using System.Runtime.CompilerServices;

namespace chessboard
{
    class Position
    {
        private int line;
        private int column;

        public int Line { get => line; set => line = value; }
        public int Column { get => column; set => column = value; }

        public Position(int line, int column)
        {
            this.line = line;
            this.column = column;
        }

        public override string ToString()
        {
            return line 
                + ", "
                + column;
        }

    }
}
