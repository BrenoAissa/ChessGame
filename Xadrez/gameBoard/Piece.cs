using System;

namespace chessboard
{
     class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movesCount { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Color color, Board board)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.movesCount = 0;
        }

        public void incrementMoveCount()
        {
            movesCount++;
        }
    }
}
