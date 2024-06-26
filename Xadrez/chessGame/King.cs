using System;
using chessboard;

namespace ChessGame
{
    class King : Piece
    {
        public King(ChessBoard board, Color color) : base(color, board) { 
        }

        public override string ToString()
        {
            return "K";
        }
    }
    
}
