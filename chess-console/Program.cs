using chess_console;
using Chesssboard;
using Chess;
using chess_console.Chesssboard;

try
{
    Chessboard board = new Chessboard(8, 8);

    board.placePiece(new Rooks(Color.Black, board), new Position(0, 0));

    Screen.printChessboard(board);

}
catch(ChessboardException e)
{
    Console.WriteLine(e.Message);
}


