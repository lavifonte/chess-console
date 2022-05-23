using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;


namespace Chess
{
    internal class Rooks : Piece
    {
        public Rooks(Chessboard chessboard, Color color) : base(chessboard, color)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
