using chessboard;
using chessGame;
using ChessGame;
using System;

namespace Xadrez.chessGame
{
    class ChessMatch
    {
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            insertPieces();
        }

        public void executeMoves(Position origin, Position destination)
        {
            Piece p = board.removePiece(origin);
            p.incrementMoveCount();
            Piece capturedPiece = board.removePiece(destination);
            board.insertPiece(p, destination);
        }

        public void insertPieces()
        {
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('c', 1).ToPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('c', 2).ToPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('d', 2).ToPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('e', 2).ToPosition());
            board.insertPiece(new Rook(board, Color.White), new ChessPosition('e', 1).ToPosition());
            board.insertPiece(new King(board, Color.White), new ChessPosition('d', 1).ToPosition());


            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('c', 7).ToPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('c', 8).ToPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('d', 7).ToPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('e', 7).ToPosition());
            board.insertPiece(new Rook(board, Color.Black), new ChessPosition('e', 8).ToPosition());
            board.insertPiece(new King(board, Color.Black), new ChessPosition('d', 8).ToPosition());

        }
    }
}
