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
        private ChessMatch Match;
        public King(Chessboard chessboard, Color color, ChessMatch match) : base(chessboard, color)
        {
            this.Match = match;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Chessboard.Piece(position);
            return piece == null || piece.Color != Color; // return true (ergo, can move) if there's no piece or if there's opponent piece
        }

        private bool TestRookCastling(Position position)
        {
            Piece piece = Chessboard.Piece(position);
            return piece != null && piece is Rook && piece.MovementCount == 0 && piece.Color == Color;  
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

            // castling short

            if(MovementCount == 0 && !Match.Check)
            {
                Position rookPosition = new Position(Position.Row, Position.Column + 3);
                if(TestRookCastling(rookPosition))
                {
                    Position tempPosition = new Position(Position.Row, Position.Column + 1);
                    Position tempPosition2 = new Position(Position.Row, Position.Column + 2);

                    if(Chessboard.Piece(tempPosition) == null && Chessboard.Piece(tempPosition2) == null)
                    {
                        movementsArray[Position.Row, Position.Column + 2] = true;
                    }
                }
            }

            // castling long

            if (MovementCount == 0 && !Match.Check)
            {
                Position rookPosition2 = new Position(Position.Row, Position.Column - 4);
                if (TestRookCastling(rookPosition2))
                {
                    Position tempPosition = new Position(Position.Row, Position.Column - 1);
                    Position tempPosition2 = new Position(Position.Row, Position.Column - 2);
                    Position tempPosition3 = new Position(Position.Row, Position.Column - 3);

                    if (Chessboard.Piece(tempPosition) == null && Chessboard.Piece(tempPosition2) == null && Chessboard.Piece(tempPosition3) == null)
                    {
                        movementsArray[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return movementsArray;
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
