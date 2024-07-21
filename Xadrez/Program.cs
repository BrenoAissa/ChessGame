using System;
using chessboard;
using ChessGame;
using Xadrez.chessBoard;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                ChessBoard board = new ChessBoard(8, 8);

                board.insertPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.insertPiece(new Rook(board, Color.Black), new Position(1, 9));
                board.insertPiece(new King(board, Color.Black), new Position(0, 2));

                Screen.printBoard(board);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
