using chess_console;
using Chesssboard;
using Chess;
using chess_console.Chesssboard;

try
{
    ChessMatch match = new ChessMatch();
    
    while(!match.End)
    {
        Console.Clear();
        Screen.printChessboard(match.Board);
        Console.WriteLine();

        Console.Write("Enter origin position: ");
        Position origin = Screen.ReadPosition().toPosition();

        bool[,] possiblePositions = match.Board.piece(origin).possibleMovements();
        Console.Clear();
        Screen.printChessboard(match.Board, possiblePositions);

        Console.WriteLine();
        Console.Write("Enter next position: ");
        Position next = Screen.ReadPosition().toPosition();

        match.Movement(origin, next);
    }   

}
catch(ChessboardException e)
{
    Console.WriteLine(e.Message);
}


