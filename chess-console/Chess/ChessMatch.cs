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
        public Piece PossiblyEnPassant { get; private set; }


        public ChessMatch()
        {
            Board = new Chessboard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            End = false;
            Check = false;
            PossiblyEnPassant = null;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            PlacePieces();          
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

            // castling short

            if(piece is King && final.Column == initial.Column + 2)
            {
                Position rookInitial = new(initial.Row, initial.Column + 3);
                Position rookFinal = new(initial.Row, initial.Column + 1);
                Piece rook = Board.RemovePiece(rookInitial);
                rook.AddMovement();
                Board.PlacePiece(rook, rookFinal);
            }

            // castling long

            if (piece is King && final.Column == initial.Column - 2)
            {
                Position rookInitial = new(initial.Row, initial.Column - 4);
                Position rookFinal = new(initial.Row, initial.Column - 1);
                Piece rook = Board.RemovePiece(rookInitial);
                rook.AddMovement();
                Board.PlacePiece(rook, rookFinal);
            }


            // en passant
            if(piece is Pawn)
            {
                if(initial.Column != final.Column && capturedPiece == null)
                {
                    Position pawnPosition;

                    if(piece.Color == Color.White)
                    {
                        pawnPosition = new Position(final.Row + 1, final.Column);
                    }

                    else
                    {
                        pawnPosition = new Position(final.Row - 1, final.Column);
                    }

                    capturedPiece = Board.RemovePiece(pawnPosition);
                    CapturedPieces.Add(capturedPiece);
                }
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

            Piece piece = Board.Piece(final);

            // promotion
            if(piece is Pawn)
            {
                if((piece.Color == Color.White && final.Row == 0) || (piece.Color == Color.Black && final.Row == 7))
                {
                    piece = Board.RemovePiece(final);
                    Pieces.Remove(piece); // removes from the board
                    Piece queen = new Queen(Board, piece.Color); // creates new queen to substitute pawn
                    Board.PlacePiece(queen, final);
                    Pieces.Add(queen);
                }
            }
            // end of promotion

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

           

            // en passant

            if(piece is Pawn && (final.Row == initial.Row - 2 || final.Row == initial.Row + 2))
            {
                PossiblyEnPassant = piece;
            }

            else
            {
                PossiblyEnPassant = null;
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

            // castling short

            if (piece is King && final.Column == initial.Column + 2)
            {
                Position rookInitial = new(initial.Row, final.Column + 3);
                Position rookFinal = new(initial.Row, final.Column + 1);
                Piece rook = Board.RemovePiece(rookFinal);
                rook.UndoMovement();
                Board.PlacePiece(rook, rookInitial);
            }

            // castling long

            if (piece is King && final.Column == initial.Column - 2)
            {
                Position rookInitial = new(initial.Row, final.Column - 4);
                Position rookFinal = new(initial.Row, final.Column - 1);
                Piece rook = Board.RemovePiece(rookFinal);
                rook.UndoMovement();
                Board.PlacePiece(rook, rookInitial);
            }

            // en passant

            if(piece is Pawn)
            {
                if(initial.Column != final.Column && capturedPiece == PossiblyEnPassant)
                {
                    Piece pawn = Board.RemovePiece(final);
                    Position positionPawn;

                    if(piece.Color == Color.White)
                    {
                        positionPawn = new Position(3, final.Column); 
                    }

                    else
                    {
                        positionPawn = new Position(4, final.Column);
                    }

                    Board.PlacePiece(pawn, positionPawn);
                }
            }
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
            HashSet<Piece> temporary = new();
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
            HashSet<Piece> temporary = new();
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
                            Position initial = piece.Position;
                            Position final = new(i, j);
                            Piece capturedPiece = Movement(initial, final);
                            bool isCheck = IsInCheck(color);
                            Undo(initial, final, capturedPiece);

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
            PlaceNewPiece('a', 1, new Rook(Board, Color.White));
            PlaceNewPiece('b', 1, new Knight(Board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('d', 1, new Queen(Board, Color.White));
            PlaceNewPiece('e', 1, new King(Board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Knight(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black, this));




        }
    }
}
