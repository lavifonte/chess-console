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

        private ChessMatch Match;
        public Pawn(Chessboard chessboard, Color color, ChessMatch match) : base(chessboard, color)
        {
            Match = match;
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
            Position position = new(0, 0);

            if (Color == Color.White)
            {
                position.Values(Position.Row - 1, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                Position tempPosition = new(Position.Row - 1, Position.Column);
                position.Values(Position.Row - 2, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position) && Chessboard.ValidPosition(tempPosition) && PositionIsAvailable(tempPosition) && MovementCount == 0)
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


                // en passant
                if (Position.Row == 3)
                {
                    Position left = new(Position.Row, Position.Column - 1);

                    if(Chessboard.ValidPosition(left) && TheresEnemy(left) && Chessboard.Piece(left) == Match.PossiblyEnPassant)
                    {
                        possibleMovements[left.Row - 1, left.Column] = true;
                    }

                    Position right = new(Position.Row, Position.Column + 1);

                    if (Chessboard.ValidPosition(right) && TheresEnemy(right) && Chessboard.Piece(right) == Match.PossiblyEnPassant)
                    {
                        possibleMovements[right.Row - 1, right.Column] = true;
                    }
                }

            }

            else
            {
                position.Values(Position.Row + 1, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position))
                {
                    possibleMovements[position.Row, position.Column] = true;
                }

                Position tempPosition = new(Position.Row + 1, Position.Column);
                position.Values(Position.Row + 2, Position.Column);
                if (Chessboard.ValidPosition(position) && PositionIsAvailable(position) && Chessboard.ValidPosition(tempPosition) && PositionIsAvailable(tempPosition) && MovementCount == 0)
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


                // en passant
                if (Position.Row == 4)
                {
                    Position left = new(Position.Row, Position.Column - 1);

                    if (Chessboard.ValidPosition(left) && TheresEnemy(left) && Chessboard.Piece(left) == Match.PossiblyEnPassant)
                    {
                        possibleMovements[left.Row + 1, left.Column] = true;
                    }

                    Position right = new(Position.Row, Position.Column + 1);

                    if (Chessboard.ValidPosition(right) && TheresEnemy(right) && Chessboard.Piece(right) == Match.PossiblyEnPassant)
                    {
                        possibleMovements[right.Row + 1, right.Column] = true;
                    }
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
