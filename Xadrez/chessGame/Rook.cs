﻿using System;
using chessboard;

namespace ChessGame
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }

}
