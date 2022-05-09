using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesssboard
{
    internal class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementCount { get; protected set; }

        public Chessboard Chessboard { get; set; }

        public Piece(Color color, Chessboard chessboard)
        {
            Position = null; //only the chessboard is responsible of placing pieces in positions. every piece starts with position null
            Color = color;
            Chessboard = chessboard;
            MovementCount = 0;
        }
    }
}
