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
            ChessPosition chessPos = new ChessPosition('a', 1);

            Console.WriteLine(chessPos);

            Console.WriteLine(chessPos.ToPosition());

            Console.ReadLine();
        }
    }
}
