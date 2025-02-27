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
                    try
                    {
                        Console.Clear();
                        Screen.printMatch(chessMatch);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.readPositionChess().ToPosition();
                        chessMatch.validatePositionOrigin(origin);

                        bool[,] possiblePositions = chessMatch.board.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printBoard(chessMatch.board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.readPositionChess().ToPosition();
                        chessMatch.validatePositionDestination(origin, destination);

                        chessMatch.makeMove(origin, destination);
                    }
                    catch(ChessBoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.printMatch(chessMatch);
            }
            catch(ChessBoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
