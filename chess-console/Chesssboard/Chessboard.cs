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
        private Pieces[,] Pieces;

        public Chessboard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Pieces[rows, columns];
        }
    }
}
