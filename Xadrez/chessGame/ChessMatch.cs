using chessboard;
using chessGame;
using ChessGame;
using System;
using Xadrez.chessBoard;

namespace Xadrez.chessGame
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool endGame { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            endGame = false;
            insertPieces();
        }

        public void executeMoves(Position origin, Position destination)
        {
            Piece p = board.removePiece(origin);
            p.incrementMoveCount();
            Piece capturedPiece = board.removePiece(destination);
            board.insertPiece(p, destination);
        }

        public void validatePositionOrigin(Position pos)
        {
            if (board.piece(pos) == null) throw new ChessBoardException("There is no piece at the chosen origin position!");
            if (currentPlayer != board.piece(pos).color) throw new ChessBoardException("The chosen origin piece is not yours!");
            if (!board.piece(pos).existsPossibleMoves()) throw new ChessBoardException("There are no possible moves for the chosen piece!");
        }

        public void validatePositionDestination(Position origin, Position destination)
        {
            if (!board.piece(origin).canMoveTo(destination)) throw new ChessBoardException("Invalid destination position");
        }

        public void makeMove(Position origin, Position destination)
        {
            executeMoves(origin, destination);
            turn++;
            changedPlayer();
        }

        private void changedPlayer()
        {
            if (currentPlayer == Color.White) currentPlayer = Color.Black;
            else currentPlayer = Color.White;
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
