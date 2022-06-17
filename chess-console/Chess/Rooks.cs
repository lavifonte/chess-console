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

        private bool canMove(Position position)
        {
            Piece piece = Chessboard.piece(position);
            return piece == null || piece.Color != Color; // return true (ergo, can move) if there's no piece or if there's opponent piece
        }

        public override bool[,] possibleMovements()
        {
            bool[,] movementsArray = new bool[Chessboard.Rows, Chessboard.Columns];

            Position position = new Position(0, 0);

            //testing each position

            position.values(Position.Row - 1, Position.Column);
            while (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if(Chessboard.piece(position) != null && Chessboard.piece(position).Color != Color)
                {
                    break;
                }

                position.Row = position.Row - 1;
            }

            position.values(Position.Row + 1, Position.Column);
            while (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if (Chessboard.piece(position) != null && Chessboard.piece(position).Color != Color)
                {
                    break;
                }

                position.Row = position.Row + 1;
            }

            position.values(Position.Row, Position.Column + 1);
            while (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if (Chessboard.piece(position) != null && Chessboard.piece(position).Color != Color)
                {
                    break;
                }

                position.Column = position.Column + 1;
            }

            position.values(Position.Row, Position.Column - 1);
            while (Chessboard.validPosition(position) && canMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if (Chessboard.piece(position) != null && Chessboard.piece(position).Color != Color)
                {
                    break;
                }

                position.Column = position.Column - 1;
            }

            return movementsArray;
        }
        public override string ToString()
        {
            return "R";
        }

    }
}
