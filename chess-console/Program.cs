using chess_console;
using Chesssboard;
using Chess;



Chessboard board = new Chessboard(8, 8);

board.placePiece(new Position(0, 0), new Rooks(Color.Black, board));

Screen.printChessboard(board);



