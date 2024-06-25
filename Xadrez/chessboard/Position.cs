
using System.Runtime.CompilerServices;

namespace chessboard
{
    class Position
    {
        private int Line;
        private int Column;

        public int line { get => Line; set => Line = value; }
        public int column { get => Column; set => Column = value; }

        public Position(int line, int column)
        {
            this.Line = line;
            this.Column = column;
        }

        public override string ToString()
        {
            return Line 
                + ", "
                + Column;
        }

    }
}
