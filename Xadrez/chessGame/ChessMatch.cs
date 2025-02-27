using chessboard;
using chessGame;
using ChessGame;
using System;
using System.Collections.Generic;
using Xadrez.chessBoard;

namespace Xadrez.chessGame
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool endGame { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            endGame = false;
            check = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            insertPieces();
        }

        public Piece executeMoves(Position origin, Position destination)
        {
            Piece p = board.removePiece(origin);
            p.incrementMoveCount();
            Piece capturedPiece = board.removePiece(destination);
            board.insertPiece(p, destination);
            if(capturedPiece != null) captured.Add(capturedPiece);
            return capturedPiece;
        }

        public void undoMove(Position origin, Position destination, Piece pieceCaptured)
        {
            Piece p = board.removePiece(destination);
            p.decrementMoveCount();
            if (pieceCaptured != null)
            {
                board.insertPiece(pieceCaptured, destination);
                captured.Remove(pieceCaptured);
            }
            board.insertPiece(p, origin);

        }

        public void makeMove(Position origin, Position destination)
        {
            Piece pieceCaptured = executeMoves(origin, destination);
            if (isInCheck(currentPlayer)) {
                undoMove(origin, destination, pieceCaptured);
                throw new ChessBoardException("You cannot put yourself in check");
            }
            if (isInCheck(adversary(currentPlayer))) check = true;
            else check = false;
            if (testCheckMate(adversary(currentPlayer))) endGame = true;
            else { 
                turn++;
                changedPlayer();
            }
        }

        public void validatePositionOrigin(Position pos)
        {
            if (board.piece(pos) == null) throw new ChessBoardException("There is no piece at the chosen origin position!");
            if (currentPlayer != board.piece(pos).color) throw new ChessBoardException("The chosen origin piece is not yours!");
            if (!board.piece(pos).existsPossibleMoves()) throw new ChessBoardException("There are no possible moves for the chosen piece!");
        }

        public void validatePositionDestination(Position origin, Position destination)
        {
            if (!board.piece(origin).possibleMove(destination)) throw new ChessBoardException("Invalid destination position");
        }

        private void changedPlayer()
        {
            if (currentPlayer == Color.White) currentPlayer = Color.Black;
            else currentPlayer = Color.White;
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in captured)
            {
                if (p.color == color) aux.Add(p);
            }
            return aux;
        }

        private Color adversary(Color color)
        {
            if (color == Color.White) return Color.Black;
            return Color.White;
        }

        private Piece king(Color color)
        {
            foreach (Piece p in piecesInGame(color))
            {
                if (p is King) return p;
            }
            return null;
        }

        public bool isInCheck(Color color)
        {

            Piece k = king(color);
            if(k.color == null) throw new ChessBoardException("There is no king of " + color + " on the board.");
                
            foreach (Piece p in piecesInGame(adversary(color))){
                bool[,] mat = p.possibleMoves();
                if (mat[k.position.row, k.position.column]) return true;
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if(!isInCheck(color)) return false;

            foreach (Piece p in piecesInGame(color))
            {
                bool[,] mat = p.possibleMoves();
                for(int i = 0; i < board.rows; i++)
                {
                    for(int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.position;
                            Position destination = new Position(i, j);
                            Piece pieceCaptured = executeMoves(origin, destination);
                            bool testCheck = isInCheck(color);
                            undoMove(origin, destination, pieceCaptured);
                            if (!testCheck) return false;
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
            {
                if (p.color == color) aux.Add(p);
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public void placeNewPiece(char column, int row, Piece piece)
        {
            board.insertPiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        public void insertPieces()
        {
            placeNewPiece('c', 1, new Rook(board, Color.White));
            placeNewPiece('d', 1, new King(board, Color.White));
            placeNewPiece('h', 7, new Rook(board, Color.White));


            placeNewPiece('a', 8, new King(board, Color.Black));
            placeNewPiece('b', 8, new Rook(board, Color.Black));

        }
    }
}
