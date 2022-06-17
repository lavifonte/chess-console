using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;

namespace Chess
{
    internal class Queen : Piece
    {
        public Queen(Chessboard chessboard, Color color) : base(chessboard, color)
        {

        }

        public override bool[,] possibleMovements()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
