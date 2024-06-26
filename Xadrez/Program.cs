using System;
using chessboard;
using ChessGame;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard(8, 8);

            board.insertPiece(new Rook(board, Color.Black), new Position(0, 0));
            board.insertPiece(new Rook(board, Color.Black), new Position(1, 3));
            board.insertPiece(new King(board, Color.Black), new Position(2, 4));

            Screen.printBoard(board);

            Console.ReadLine();
        }
    }
}
