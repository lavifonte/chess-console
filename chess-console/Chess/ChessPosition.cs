using Chesssboard;

namespace Chess
{
    internal class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public Position toPosition()
        {
            return new Position(8 - Row, Column - 'a'); //in the array, the equivalent chessboard row position would be
                                                        //8 - {chess row position} bc the position 8 on the chessboard would be 0 on the array (so 8 - 8 = 0)
                                                        // and the equivalent chessboard column position would be {chess column position} - 'a'
                                                        //bc the char 'a' is like the first digit (0) so ('a' - 'a' = 0)
        }

        public override string ToString()
        {
            return $" {Column}{Row}";
        }
    }
}
