using System;
using chessboard;
using chessGame;
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
                Board board = new Board(8, 8);

                board.insertPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.insertPiece(new Rook(board, Color.Black), new Position(1, 3));
                board.insertPiece(new King(board, Color.Black), new Position(0, 2));

                board.insertPiece(new Rook(board, Color.White), new Position(3, 5));

                Screen.printBoard(board);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
