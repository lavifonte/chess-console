using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesssboard
{
    internal class Pieces
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementCount { get; protected set; }

        public Chessboard Chessboard { get; set; }

        public Pieces(Color color, Chessboard chessboard)
        {
            Position = null;
            Color = color;
            Chessboard = chessboard;
            MovementCount = 0;
        }
    }
}
