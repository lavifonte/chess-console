using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;
using Chesssboard;

namespace chess_console
{
    internal class Screen
    {
        public static void printChessboard(Chessboard board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                   printPiece(board.piece(i, j));                     
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");

        }

        public static void printChessboard(Chessboard board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if(possiblePositions[i,j])
                    {
                        Console.BackgroundColor = alteredBackground;
                    }

                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }

                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;

        }

        public static ChessPosition ReadPosition()
        {
            string typed = Console.ReadLine();
            char column = typed[0];
            int row = int.Parse(typed[1] + "");
            return new ChessPosition(column, row);
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }

                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }
    }
}
