using System;
using chessboard;
using chessGame;
using ChessGame;
using Xadrez.chessBoard;
using Xadrez.chessGame;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                Screen.printBoard(chessMatch.board);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
