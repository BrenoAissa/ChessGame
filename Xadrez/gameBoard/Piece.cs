using System;

namespace chessboard
{
    abstract class Piece
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

        public void decrementMoveCount()
        {
            movesCount--;
        }

        public bool existsPossibleMoves()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < board.rows; i++)
            {
                for(int j = 0; j < board.columns; j++){
                    if (mat[i, j]) return true;
                }
            }
            return false;
        }

        public bool possibleMove(Position pos)
        {
            return possibleMoves()[pos.row, pos.column];
        }

        public abstract bool[,] possibleMoves();
    }
}
