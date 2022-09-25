namespace Bingo.Domain.Models;

//TODO: Implement the square label as the string. -1 for wrong, 0 for skipped, 1 for correct.
public sealed record PlayerPerSquareResult(string SquareLabel, short Result);