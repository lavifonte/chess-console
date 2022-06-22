using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chess_console.Chesssboard;
using Chesssboard;

namespace Chess
{
    internal class ChessMatch
    {
        public Chessboard Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
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
            Piece piece = Board.RemovePiece(initial);
            piece.AddMovement();
            Board.RemovePiece(final);
            Board.PlacePiece(piece, final);
        }

        public void Move(Position initial, Position final) // completes a move and go to next turn
        {
            Movement(initial, final);
            Turn++;
            ChangePlayer();
        }

        public void ValidateInitialPosition(Position initial)
        {
            if(Board.Piece(initial) == null)
            {
                throw new ChessboardException("There's no piece in that position!");
            }

            if(CurrentPlayer != Board.Piece(initial).Color)
            {
                throw new ChessboardException("That's not your piece!");
            }

            if(!Board.Piece(initial).TheresPossibleMovements())
            {
                throw new ChessboardException("No possible movements for that piece!");
            }
        }

        public void ValidateFinalPosition(Position initial, Position final)
        {
            if(!Board.Piece(initial).CanMoveTo(final))
            {
                throw new ChessboardException("Can't go there!");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }

            else
            {
                CurrentPlayer = Color.White;
            }
        }
        private void PlacePieces()
        {
            Board.PlacePiece(new Rooks(Board, Color.White), new ChessPosition('a', 1).ToPosition());
            Board.PlacePiece(new Rooks(Board, Color.White), new ChessPosition('h', 1).ToPosition());
            Board.PlacePiece(new Rooks(Board, Color.Black), new ChessPosition('a', 8).ToPosition());
            Board.PlacePiece(new Rooks(Board, Color.Black), new ChessPosition('h', 8).ToPosition());
            Board.PlacePiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());
            Board.PlacePiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
