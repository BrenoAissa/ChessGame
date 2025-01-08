using System;

namespace Xadrez.chessBoard
{
    class ChessBoardException : Exception
    {
        public ChessBoardException(string message) : base(message) { }
    }
}
