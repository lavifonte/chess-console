using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesssboard
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementCount { get; protected set; }

        public Chessboard Chessboard { get; set; }

        public Piece(Chessboard chessboard, Color color)
        {
            Position = null; //only the chessboard is responsible of placing pieces in positions. every piece starts with position null
            Color = color;
            Chessboard = chessboard;
            MovementCount = 0;
        }

        public abstract bool[,] possibleMovements(); // abstract because the logic of this method depends on each piece
       
        public void addMovement()
        {
            MovementCount++;   
        }
    }
}
