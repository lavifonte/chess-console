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
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CapturedPieces;

        public ChessMatch()
        {
            Board = new Chessboard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            End = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();  
            PlacePieces();
        }

        public void Movement(Position initial, Position final)
        {
            Piece piece = Board.RemovePiece(initial);
            piece.AddMovement();
            Piece capturedPiece = Board.RemovePiece(final);
            Board.PlacePiece(piece, final);

            if(capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
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

        public HashSet<Piece> Captured(Color color)
        {
            HashSet<Piece> temporary = new HashSet<Piece>();
            foreach(Piece piece in CapturedPieces)
            {
                if(piece.Color == color)
                {
                    temporary.Add(piece);
                }
            }

            return temporary;
        }

        public HashSet<Piece> OnBoard(Color color) // pieces still on the board and playing, not yet removed
        {
            HashSet<Piece> temporary = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    temporary.Add(piece);
                }
            }

            temporary.ExceptWith(Captured(color));
            return temporary;
        }
        public void PlaceNewPiece(char column, int row, Piece piece)
        {
            Board.PlacePiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }
        private void PlacePieces()
        {
            PlaceNewPiece('a', 1, new Rooks(Board, Color.White));
            PlaceNewPiece('h', 1, new Rooks(Board, Color.White));
            PlaceNewPiece('a', 8, new Rooks(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rooks(Board, Color.Black));
            PlaceNewPiece('d', 1, new Rooks(Board, Color.White));
            PlaceNewPiece('d', 8, new Rooks(Board, Color.Black));
        }
    }
}
