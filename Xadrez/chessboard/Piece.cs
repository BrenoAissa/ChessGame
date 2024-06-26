using System;

namespace chessboard
{
     class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int moves { get; protected set; }
        public ChessBoard board { get; protected set; }

        public Piece(Color color, ChessBoard board)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.moves = 0;
        }
    }
}
