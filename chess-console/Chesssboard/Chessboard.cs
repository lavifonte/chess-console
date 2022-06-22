using chess_console.Chesssboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chesssboard
{
    internal class Chessboard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        private Piece[,] Pieces; // the board contains an array of Pieces[row, column] - multiple pieces placed in specific rows and columns

        public Chessboard(int rows, int columns)
        {
            Rows = rows; //total rows in the board
            Columns = columns; //total columns in the board
            Pieces = new Piece[rows, columns];
        }

        public Piece Piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            return Pieces[position.Row, position.Column];
        }

        public bool TheresPiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void PlacePiece(Piece piece, Position position) // places piece x in the position y
        {
            if (TheresPiece(position))
            {
                throw new ChessboardException("There's a piece in this position");
            }

            Pieces[position.Row, position.Column] = piece; // this position on the board receives this piece
            piece.Position = position;

        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }

            Piece temporary = Piece(position);
            temporary.Position = null;
            Pieces[position.Row, position.Column] = null;
            return temporary;

        }
        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new ChessboardException("Invalid position");
            }
        }
    }
}
