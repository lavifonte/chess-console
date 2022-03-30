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

        private Pieces[,] Piece;

        public Chessboard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Piece = new Pieces[rows, columns];
        }

        public Pieces piece(int rows, int columns)
        {
            return Piece[rows, columns];
        }

        public void placePiece(Position position, Pieces piece)
        {
            Piece[position.Row, position.Column] = piece;
            piece.Position = position;

        }
    }
}
