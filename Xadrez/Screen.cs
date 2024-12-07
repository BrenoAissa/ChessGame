using chessboard;
using chessGame;
using System;

namespace Chess
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (board.piece(i, j) == null) Console.Write("- ");

                    else
                    {
                        printPiece(board.piece(i, j));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition readPositionChess()
        {
            string input = Console.ReadLine();
            char letterColumn = input[0];
            int numberRow = int.Parse(input[1] + "");
            return new ChessPosition(letterColumn, numberRow);
        }

        public static void printPiece(Piece piece)
        {
            if(piece.color == Color.White) Console.Write(piece);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
