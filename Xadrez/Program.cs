using System;
using chessboard;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard(8, 8);

            Screen.printBoard(board);

            Console.ReadLine();
        }
    }
}
