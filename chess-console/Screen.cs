using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;

namespace chess_console
{
    internal class Screen
    {
        public static void printChessboard(Chessboard board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.piece(i, j) == null) // if there's no piece in this position(r,c)
                    {
                        Console.Write("- ");
                    }

                    else
                    {
                        Console.Write($"{board.piece(i, j)} ");
                    }

                }

                Console.WriteLine(); // break line for the next column
            }

        }
    }
}
