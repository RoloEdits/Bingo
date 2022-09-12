namespace Bingo.Library;

public interface IGrid
{
    // TODO - Check to see if label generation should go here.
    // Could concat to the column/row number and use this throughout the program for better context.
    // Like for the Game.CorrectGuessesPerSquare. Dictionary<string squareLabel, int correctGuesses>
    public byte Columns { get; init; }
    public byte Rows { get; init; }
    public byte TotalSquares { get; init; }
}
