using chess_console;
using Chesssboard;
using Chess;
using chess_console.Chesssboard;

try
{
    ChessMatch match = new ChessMatch();

    while (!match.End)
    {
        try
        {
            Console.Clear();
            Screen.PrintMatch(match);

            Console.WriteLine();
            Console.Write("Enter origin position: ");
            Position initial = Screen.ReadPosition().ToPosition();
            match.ValidateInitialPosition(initial);

            bool[,] possiblePositions = match.Board.Piece(initial).PossibleMovements();
            Console.Clear();
            Screen.PrintChessboard(match.Board, possiblePositions);

            Console.WriteLine();
            Console.Write("Enter next position: ");
            Position next = Screen.ReadPosition().ToPosition();
            match.ValidateFinalPosition(initial, next);
            match.Move(initial, next);
        }
        catch (ChessboardException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }

}
catch (ChessboardException e)
{
    Console.WriteLine(e.Message);
}


