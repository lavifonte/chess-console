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
                    if (board.piece(i, j) == null) // if there's no piece in this position(r,c)
                    {
                        Console.Write("- ");
                    }

                    else
                    {
                        printPiece(board.piece(i, j));                        
                    }                                      

                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");



        }

        public static ChessPosition ReadPosition()
        {
            string typed = Console.ReadLine();
            char column = typed[0];
            int row = int.Parse(typed[1] + " ");
            return new ChessPosition(column, row);
        }

        public static void printPiece(Piece piece)
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
