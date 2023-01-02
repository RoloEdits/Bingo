using Bingo.Domain.Models;

namespace Bingo.Domain.Errors;

[Serializable]
public class InvalidGuessersException : Exception
{
    public List<InvalidGuesser>? InvalidGuessers { get; }

    public InvalidGuessersException()
    {
    }

    public InvalidGuessersException(List<InvalidGuesser> invalidGuessers)
    {
        InvalidGuessers = invalidGuessers;
    }

    public InvalidGuessersException(string message) : base(message)
    {
    }
}
