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

        private bool CanMove(Position position)
        {
            Piece piece = Chessboard.Piece(position);
            return piece == null || piece.Color != Color; // return true (ergo, can move) if there's no piece or if there's opponent piece
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementsArray = new bool[Chessboard.Rows, Chessboard.Columns];

            Position position = new Position(0, 0);

            //testing each position

            position.Values(Position.Row - 1, Position.Column);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if(Chessboard.Piece(position) != null && Chessboard.Piece(position).Color != Color)
                {
                    break;
                }

                position.Row--;
            }

            position.Values(Position.Row + 1, Position.Column);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if (Chessboard.Piece(position) != null && Chessboard.Piece(position).Color != Color)
                {
                    break;
                }

                position.Row++;
            }

            position.Values(Position.Row, Position.Column + 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if (Chessboard.Piece(position) != null && Chessboard.Piece(position).Color != Color)
                {
                    break;
                }

                position.Column++;
            }

            position.Values(Position.Row, Position.Column - 1);
            while (Chessboard.ValidPosition(position) && CanMove(position))
            {
                movementsArray[position.Row, position.Column] = true;

                if (Chessboard.Piece(position) != null && Chessboard.Piece(position).Color != Color)
                {
                    break;
                }

                position.Column--;
            }

            return movementsArray;
        }
        public override string ToString()
        {
            return "R";
        }

    }
}
