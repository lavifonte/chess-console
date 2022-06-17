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

        private bool canMove(Position position)
        {
            Piece piece = Chessboard.piece(position);
            return piece == null || piece.Color != Color; // return true (ergo, can move) if there's no piece or if there's opponent piece
        }

        public override bool[,] possibleMovements()
        {
            bool[,] movementsArray = new bool[Chessboard.Rows, Chessboard.Columns];

            Position position = new Position(0, 0);

            //testing each position, in each direction (because the king can move one time to any direction

            position.values(Position.Row - 1, Position.Column);
            if(Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;  
            }

            position.values(Position.Row - 1, Position.Column + 1);
            if (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.values(Position.Row, Position.Column + 1);
            if (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.values(Position.Row + 1, Position.Column + 1);
            if (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.values(Position.Row + 1, Position.Column);
            if (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.values(Position.Row + 1, Position.Column - 1);
            if (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.values(Position.Row, Position.Column - 1);
            if (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;
            }

            position.values(Position.Row - 1, Position.Column - 1);
            if (Chessboard.validPosition(position) && canMove(position))
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
