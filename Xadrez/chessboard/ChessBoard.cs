﻿using System;
using Xadrez.chessBoard;

namespace chessboard
{
    class ChessBoard
    {
        public int lines {  get; set; }
        public int columns { get; set; }
        private Piece[,] pieces { get; set; }

        public ChessBoard(int lines, int columns)
        {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece piece(int line, int column)
        {
            return pieces[line, column];
        }
        
        public Piece piece(Position pos)
        {
            return pieces[pos.line, pos.column];
        }

        public bool existPiece(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        }

        public void insertPiece(Piece p, Position pos)
        {
            if (existPiece(pos))
            {
                throw new BoardException("There is already a piece in this position");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        public bool validPosition(Position pos)
        {
            if(pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns) return false;

            return true;
        }

        public void validatePosition(Position pos)
        {
            if (!validPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
