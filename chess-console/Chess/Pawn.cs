using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;

namespace Chess
{
    internal class Pawn : Piece
    {
        public Pawn(Chessboard chessboard, Color color) : base(chessboard, color)
        {
        }

        private bool TheresEnemy(Position position)
        {
            Piece piece = Chessboard.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool PositionIsAvailable(Position position)
        {
            return Chessboard.Piece(position) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] possibleMovements = new bool[Chessboard.Rows, Chessboard.Columns];
            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                position.Values(Position.Row - 1, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                position.Values(Position.Row - 2, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position) && MovementCount == 0)
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                position.Values(Position.Row - 1, Position.Column - 1);
                if (Chessboard.ValidPosition(position) && TheresEnemy(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                position.Values(Position.Row - 1, Position.Column + 1);
                if (Chessboard.ValidPosition(position) && TheresEnemy(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }
            }

            else
            {
                position.Values(Position.Row + 1, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                position.Values(Position.Row + 2, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position) && MovementCount == 0)
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                position.Values(Position.Row + 1, Position.Column - 1);
                if (Chessboard.ValidPosition(position) && TheresEnemy(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                position.Values(Position.Row + 1, Position.Column + 1);
                if (Chessboard.ValidPosition(position) && TheresEnemy(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }
            }

            return possibleMovements;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
