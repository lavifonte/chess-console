using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;

namespace Chess
{
    internal class Knight : Piece
    {
        public Knight(Chessboard chessboard, Color color) : base(chessboard, color)
        {
        }


        private bool CanMove(Position position)
        {
            Piece piece = Chessboard.Piece(position);
            return piece == null || piece.Color != Color; // return true (ergo, can move) if there's no piece or if there's opponent piece
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] possibleMovements = new bool[Chessboard.Rows, Chessboard.Columns];
            Position position = new Position(0, 0);

            position.Values(Position.Row - 1, Position.Column - 2);
            if(Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            position.Values(Position.Row - 2, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            position.Values(Position.Row - 2, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            position.Values(Position.Row - 1, Position.Column + 2);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            position.Values(Position.Row + 1, Position.Column + 2);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            position.Values(Position.Row + 2, Position.Column + 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            position.Values(Position.Row + 2, Position.Column - 1);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            position.Values(Position.Row + 1, Position.Column - 2);
            if (Chessboard.ValidPosition(position) && CanMove(position))
            {
                possibleMovements[position.Row, position.Column] = true;    //it is a possible movement               
            }

            return possibleMovements;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
