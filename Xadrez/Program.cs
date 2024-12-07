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

                while (!chessMatch.endGame)
                {
                    Console.Clear();
                    Screen.printBoard(chessMatch.board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.readPositionChess().ToPosition();
                    Console.Write("Destination: ");
                    Position destination = Screen.readPositionChess().ToPosition();

                    chessMatch.executeMoves(origin, destination);
                }

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
