namespace Bingo.Library;

public interface IGrid
{
    byte Columns { get; init; }
    byte Rows { get; init; }
    short TotalSquares { get; init; }
}