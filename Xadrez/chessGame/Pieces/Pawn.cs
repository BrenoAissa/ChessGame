using System;
using chessboard;
using Xadrez.chessGame;

namespace ChessGame
{
    class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(color, board)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }
        
        private bool avaiable(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.defineValues(position.row - 1, position.column);
                if (board.validPosition(pos) && avaiable(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                pos.defineValues(position.row - 2, position.column);
                if (board.validPosition(pos) && avaiable(pos) && movesCount == 0)
                {
                    mat[pos.row, pos.column] = true;
                }

                pos.defineValues(position.row - 1, position.column - 1);
                if (board.validPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                pos.defineValues(position.row - 1, position.column + 1);
                if (board.validPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                // #En passant
                if (position.row == 3)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.validPosition(left) && existEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                        mat[left.row, left.column] = true;

                    Position right = new Position(position.row, position.column + 1);
                    if (board.validPosition(left) && existEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                        mat[right.row, right.column] = true;

                }

            }
            else
            {
                pos.defineValues(position.row + 1, position.column);
                if (board.validPosition(pos) && avaiable(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                pos.defineValues(position.row + 2, position.column);
                if (board.validPosition(pos) && avaiable(pos) && movesCount == 0)
                {
                    mat[pos.row, pos.column] = true;
                }

                pos.defineValues(position.row + 1, position.column - 1);
                if (board.validPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                pos.defineValues(position.row + 1, position.column + 1);
                if (board.validPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
            }

            // #En passant
            if (position.row == 4)
            {
                Position left = new Position(position.row, position.column - 1);
                if (board.validPosition(left) && existEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    mat[left.row, left.column] = true;

                Position right = new Position(position.row, position.column + 1);
                if (board.validPosition(left) && existEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    mat[right.row, right.column] = true;

            }

            return mat;
        }
    }

}
