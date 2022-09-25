using Bingo.Domain.Models;

namespace Bingo.Domain.Errors;

[Serializable]
public class InvalidUserGuessAmountException : Exception
{
    public List<InvalidGuesser> InvalidGuessers { get; }

    public InvalidUserGuessAmountException() { }

    public InvalidUserGuessAmountException(string message) : base(message) { }

    public InvalidUserGuessAmountException(string message, Exception inner) : base(message, inner) { }

    public InvalidUserGuessAmountException(string message, List<InvalidGuesser> invalidGuessers) : this(message)
    {
        InvalidGuessers = invalidGuessers;
    }
}
