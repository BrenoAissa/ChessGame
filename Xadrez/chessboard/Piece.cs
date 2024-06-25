using System;

namespace chessboard
{
     class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int moves { get; protected set; }
        public ChessBoard chessBoard { get; protected set; }

        public Piece(Position position, Color color, ChessBoard chessBoard)
        {
            this.position = position;
            this.color = color;
            this.moves = moves;
            this.chessBoard = chessBoard;
            this.moves = 0;
        }
    }
}
