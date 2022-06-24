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
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Chessboard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            End = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            PlacePieces();
            Check = false;
        }

        public Piece Movement(Position initial, Position final)
        {
            Piece piece = Board.RemovePiece(initial);
            piece.AddMovement();
            Piece capturedPiece = Board.RemovePiece(final);
            Board.PlacePiece(piece, final);

            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void Move(Position initial, Position final) // completes a move to go to next turn
        {
            Piece capturedPiece = Movement(initial, final);

            if (IsInCheck(CurrentPlayer))
            {
                Undo(initial, final, capturedPiece);
                throw new ChessboardException("You'll be in check if you go there! Press enter to try again.");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }

            else
            {
                Check = false;
            }

            if (TestIfCheckMate(Opponent(CurrentPlayer))) // if opponent is in checkmate, game is over
            {
                End = true;
            }

            else
            {
                Turn++;
                ChangePlayer();
            }

        }

        public void Undo(Position initial, Position final, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(final);
            piece.UndoMovement();

            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, final);
                CapturedPieces.Remove(capturedPiece);
            }

            Board.PlacePiece(piece, initial);

        }

        public void ValidateInitialPosition(Position initial)
        {
            if (Board.Piece(initial) == null)
            {
                throw new ChessboardException("There's no piece in that position!");
            }

            if (CurrentPlayer != Board.Piece(initial).Color)
            {
                throw new ChessboardException("That's not your piece!");
            }

            if (!Board.Piece(initial).TheresPossibleMovements())
            {
                throw new ChessboardException("No possible movements for that piece!");
            }
        }

        public void ValidateFinalPosition(Position initial, Position final)
        {
            if (!Board.Piece(initial).CanMoveTo(final))
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
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
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

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }

            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece piece in OnBoard(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);

            if (king == null)
            {
                throw new ChessboardException("There's no king!");
            }

            foreach (Piece piece in OnBoard(Opponent(color)))
            {
                bool[,] possiblemoves = piece.PossibleMovements();

                if (possiblemoves[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TestIfCheckMate(Color color) // checkmate is when there's no possible moves to get out of check
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in OnBoard(color))
            {
                bool[,] possibleMoves = piece.PossibleMovements();

                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMoves[i, j]) // returns true, meaning there's possible movements
                        {
                            Position final = new Position(i, j);
                            Piece capturedPiece = Movement(piece.Position, final);
                            bool isCheck = IsInCheck(color);
                            Undo(piece.Position, final, capturedPiece);

                            if (!isCheck) // means Movement() managed to undo check, so it's not checkmate
                            {
                                return false; // not checkmate
                            }
                        }
                    }
                }
            }

            return true; // is checkmate
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
            PlaceNewPiece('d', 1, new King(Board, Color.White));
            PlaceNewPiece('d', 8, new King(Board, Color.Black));
        }
    }
}
