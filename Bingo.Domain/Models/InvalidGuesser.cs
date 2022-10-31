namespace Bingo.Domain.Models;

public sealed record InvalidGuesser(ushort Row, string Name, int GuessAmount);
