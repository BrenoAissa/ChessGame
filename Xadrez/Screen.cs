using chessboard;
using chessGame;
using System;
using System.Collections.Generic;
using Xadrez.chessGame;

namespace Chess
{
    class Screen
    {

        public static void printMatch(ChessMatch chessMatch)
        {
            printBoard(chessMatch.board);
            Console.WriteLine();
            printPiecesCaptured(chessMatch);
            Console.WriteLine();
            Console.WriteLine("Turn: " + chessMatch.turn);
            Console.WriteLine("Waiting player " + chessMatch.currentPlayer);
        }

        public static void printPiecesCaptured(ChessMatch chessMatch)
        {
            Console.WriteLine("Pieces captured:");

            Console.Write("White: ");
            printCollection(chessMatch.capturedPieces(Color.White));
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;

            Console.Write("Black: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            printCollection(chessMatch.capturedPieces(Color.Black));

            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printCollection(HashSet<Piece> piece)
        {
            Console.Write("[");
            foreach(Piece p in piece)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.Green;

            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (possiblePositions[i, j]) Console.BackgroundColor = changedBackground;
                    else Console.BackgroundColor = originalBackground;
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition readPositionChess()
        {
            string input = Console.ReadLine();
            char letterColumn = input[0];
            letterColumn = Char.ToLower(letterColumn);
            int numberRow = int.Parse(input[1] + "");
            return new ChessPosition(letterColumn, numberRow);
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null) Console.Write("- ");
            else
            {
                if (piece.color == Color.White) Console.Write(piece);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
