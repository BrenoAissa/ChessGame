﻿using chessboard;

namespace chessGame
{
    class ChessPosition
    {
        public char column { get; set; }
        public int row { get; set; }

        public ChessPosition(char column, int row)
        {
            this.column = column;
            this.row = row;
        }

        public Position ToPosition()
        {
            return new Position(8 - row, column - 'a');
        }

        public override string ToString()
        {
            return "" + column + row;
        }
    }
}
