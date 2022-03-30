﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chesssboard;


namespace Chess
{
    internal class Rooks : Pieces
    {
        public Rooks(Color color, Chessboard chessboard) : base(color, chessboard)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
