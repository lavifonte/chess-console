using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;

namespace Chess
{
    internal class King : Piece
    {
        public King(Chessboard chessboard, Color color) : base(chessboard, color)
        {
        }

        private bool CanMove(Position position)
        {
            Piece piece = Chessboard.Piece(position);
            return piece == null || piece.Color != Color; // return true (ergo, can move) if there's no piece or if there's opponent piece
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementsArray = new bool[Chessboard.Rows, Chessboard.Columns];

            Position position = new Position(0, 0);

            //testing each position, in each direction (because the king can move one time to any direction

            position.Values(Position.Row - 1, Position.Column);
            if(Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;  
            }

            position.Values(Position.Row - 1, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.Values(Position.Row, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.Values(Position.Row + 1, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.Values(Position.Row + 1, Position.Column);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.Values(Position.Row + 1, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.Values(Position.Row, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.Values(Position.Row - 1, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            return movementsArray;
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
