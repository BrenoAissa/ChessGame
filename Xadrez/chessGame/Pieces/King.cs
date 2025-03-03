using System;
using chessboard;
using Xadrez.chessGame;

namespace ChessGame
{
    class King : Piece
    {
        private ChessMatch match;
        public King(Board board, Color color, ChessMatch match) : base(color, board) { 
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece piece = board.piece(pos);
            return piece == null || piece.color != color;
        }

        private bool testCastling(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.color == color && p.movesCount == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            pos.defineValues(position.row -1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            pos.defineValues(position.row - 1, position.column +1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            pos.defineValues(position.row, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            pos.defineValues(position.row + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            pos.defineValues(position.row + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            pos.defineValues(position.row + 1, position.column -1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            pos.defineValues(position.row, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            pos.defineValues(position.row - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // #kingside castling

            if (movesCount == 0 && !match.check) {
                Position posKingSide = new Position(position.row, position.column + 3);
                if (testCastling(posKingSide))
                {
                    Position kingMoreOne = new Position(position.row, position.column + 1);
                    Position kingMoreTwo = new Position(position.row, position.column + 2);
                    if (board.piece(kingMoreOne) == null && board.piece(kingMoreTwo) == null)
                        mat[position.row, position.column + 2] = true;
                }

                // #queenside castling
                Position posQueenSide = new Position(position.row, position.column - 4);
                if (testCastling(posQueenSide))
                {
                    Position kingMoreOne = new Position(position.row, position.column - 1);
                    Position kingMoreTwo = new Position(position.row, position.column - 2);
                    Position kingMoreThree = new Position(position.row, position.column - 3);
                    if (board.piece(kingMoreOne) == null && board.piece(kingMoreTwo) == null && board.piece(kingMoreThree) == null)
                        mat[position.row, position.column - 2] = true;
                }
            }

            return mat;
        }
    }
    
}
