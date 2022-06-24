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

        public void AddMovement()
        {
            MovementCount++;
        }

        public void UndoMovement()
        {
            MovementCount--;
        }
        public bool TheresPossibleMovements()
        {
            bool[,] temporary = PossibleMovements();

            for(int i = 0; i < Chessboard.Rows; i++)
            {
                for (int j = 0; j < Chessboard.Columns; j++)
                {
                    if(temporary[i,j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveTo(Position final)
        {
            return PossibleMovements()[final.Row, final.Column];
        }
        public abstract bool[,] PossibleMovements(); // abstract because the logic of this method depends on each piece
       
        
    }
}
