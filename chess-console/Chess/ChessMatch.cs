using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;

namespace Chess
{
    internal class ChessMatch
    {
        public Chessboard Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool End { get; private set; }

        public ChessMatch()
        {
            Board = new Chessboard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            End = false;
            PlacePieces();
        }

        public void Movement(Position initial, Position final)
        {
            Piece piece = Board.removePiece(initial);
            piece.addMovement();
            Board.removePiece(final);
            Board.placePiece(piece, final);
        }

        private void PlacePieces()
        {
            Board.placePiece(new Rooks(Board, Color.Black), new ChessPosition('c', 1).toPosition());
        }
    }
}
