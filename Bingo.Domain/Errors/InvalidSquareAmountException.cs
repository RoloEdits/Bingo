using Bingo.Domain.Models;

namespace Bingo.Domain.Errors;

[Serializable]
public class InvalidSquareAmountException : Exception
{
    public InvalidSquareAmountException()
    {
    }

    public InvalidSquareAmountException(string message) : base(message)
    {
    }

    public InvalidSquareAmountException(string message, Exception inner) : base(message, inner)
    {
    }
}