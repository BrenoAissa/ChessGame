using System;

namespace Xadrez.chessBoard
{
    class BoardException : Exception
    {
        public BoardException(string message) : base(message) { }
    }
}
