﻿using chessboard;
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
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            endGame = false;
            check = false;
            vulnerableEnPassant = null;
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

            // #kingside castling
            if(p is King && destination.column == origin.column + 2)
            {
                Position originR = new Position(origin.row, origin.column + 3);
                Position destinationR = new Position(origin.row, origin.column + 1);
                Piece R = board.removePiece(originR);
                R.incrementMoveCount();
                board.insertPiece(R, destinationR);
            }

            // #queenside castling
            if (p is King && destination.column == origin.column - 2)
            {
                Position originR = new Position(origin.row, origin.column - 4);
                Position destinationR = new Position(origin.row, origin.column - 1);
                Piece R = board.removePiece(originR);
                R.incrementMoveCount();
                board.insertPiece(R, destinationR);
            }

            // #En passant
            if(p is Pawn)
                if(origin.column != destination.column && capturedPiece == null) {
                    Position posP;
                    if (p.color == Color.White)
                        posP = new Position(destination.row + 1, destination.column);
                    else
                        posP = new Position(destination.row - 1, destination.column);

                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }

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

            // #kingside castling
            if (p is King && destination.column == origin.column + 2)
            {
                Position originR = new Position(origin.row, origin.column + 3);
                Position destinationR = new Position(origin.row, origin.column + 1);
                Piece R = board.removePiece(destinationR);
                R.decrementMoveCount();
                board.insertPiece(R, originR);
            }

            // #queenside castling
            if (p is King && destination.column == origin.column - 2)
            {
                Position originR = new Position(origin.row, origin.column - 4);
                Position destinationR = new Position(origin.row, origin.column - 1);
                Piece R = board.removePiece(destinationR);
                R.decrementMoveCount();
                board.insertPiece(R, originR);
            }

            // #En passant
            if (p is Pawn)
                if (origin.column != destination.column && pieceCaptured == vulnerableEnPassant)
                {
                    Piece pawn = board.removePiece(destination);
                    Position posP;
                    if (p.color == Color.White)
                        posP = new Position(3, destination.column);
                    else
                        posP = new Position(4, destination.column);
                    board.insertPiece(pawn, posP);
                }
        }

        public void makeMove(Position origin, Position destination)
        {
            Piece pieceCaptured = executeMoves(origin, destination);
            
            if (isInCheck(currentPlayer)) {
                undoMove(origin, destination, pieceCaptured);
                throw new ChessBoardException("You cannot put yourself in check");
            }

            Piece p = board.piece(destination);

           // #Pawn promotion
           if(p is Pawn)
                if((p.color == Color.White && destination.row == 0) || (p.color == Color.Black && destination.row == 7))
                {
                    p = board.removePiece(destination);
                    pieces.Remove(p);
                    choosePromotionPiece(destination, p);
                }

            if (isInCheck(adversary(currentPlayer))) check = true;
            else check = false;


            if (testCheckMate(adversary(currentPlayer))) endGame = true;
            else { 
                turn++;
                changedPlayer();
            }


            // #En Passant
            if (p is Pawn && (destination.row == origin.row - 2 || destination.row == origin.row + 2))
                vulnerableEnPassant = p;
            else
                vulnerableEnPassant = null;
        }

        public void choosePromotionPiece(Position destination, Piece p)
        {
            Console.WriteLine("Which piece do you want to choose? Q, B, K or R");
            string pieceChoosed = Console.ReadLine();
            string pieceChoosedFormated = pieceChoosed.ToUpper();
            if (pieceChoosedFormated == "Q")
            {
                Piece queen = new Queen(board, p.color);
                board.insertPiece(queen, destination);
                pieces.Add(queen);
            }
            else if (pieceChoosedFormated == "B")
            {
                Piece bishop = new Bishop(board, p.color);
                board.insertPiece(bishop, destination);
                pieces.Add(bishop);
            }
            else if (pieceChoosedFormated == "K")
            {
                Piece knight = new Knight(board, p.color);
                board.insertPiece(knight, destination);
                pieces.Add(knight);
            }
            else if (pieceChoosedFormated == "R")
            {
                Piece rook = new Rook(board, p.color);
                board.insertPiece(rook, destination);
                pieces.Add(rook);
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
            placeNewPiece('a', 1, new Rook(board, Color.White));
            placeNewPiece('b', 1, new Knight(board, Color.White));
            placeNewPiece('c', 1, new Bishop(board, Color.White));
            placeNewPiece('d', 1, new Queen(board, Color.White));
            placeNewPiece('e', 1, new King(board, Color.White, this));
            placeNewPiece('f', 1, new Bishop(board, Color.White));
            placeNewPiece('g', 1, new Knight(board, Color.White));
            placeNewPiece('h', 1, new Rook(board, Color.White));
            placeNewPiece('a', 2, new Pawn(board, Color.White, this));
            placeNewPiece('b', 2, new Pawn(board, Color.White, this));
            placeNewPiece('c', 2, new Pawn(board, Color.White, this));
            placeNewPiece('d', 2, new Pawn(board, Color.White, this));
            placeNewPiece('e', 2, new Pawn(board, Color.White, this));
            placeNewPiece('f', 2, new Pawn(board, Color.White, this));
            placeNewPiece('g', 2, new Pawn(board, Color.White, this));
            placeNewPiece('h', 2, new Pawn(board, Color.White, this ));


            placeNewPiece('a', 8, new Rook(board, Color.Black));
            placeNewPiece('b', 8, new Knight(board, Color.Black));
            placeNewPiece('c', 8, new Bishop(board, Color.Black));
            placeNewPiece('d', 8, new Queen(board, Color.Black)); 
            placeNewPiece('e', 8, new King(board, Color.Black, this));
            placeNewPiece('f', 8, new Bishop(board, Color.Black));
            placeNewPiece('g', 8, new Knight(board, Color.Black));
            placeNewPiece('h', 8, new Rook(board, Color.Black));
            placeNewPiece('a', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('b', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('c', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('d', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('e', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('f', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('g', 7, new Pawn(board, Color.Black, this));
            placeNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
