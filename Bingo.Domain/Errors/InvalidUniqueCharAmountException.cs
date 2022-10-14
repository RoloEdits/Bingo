namespace Bingo.Domain.Errors;

[Serializable]
public class InvalidUniqueCharAmountException : Exception
{
    public InvalidUniqueCharAmountException() { }

    public InvalidUniqueCharAmountException(string message) : base(message) { }

    public InvalidUniqueCharAmountException(string message, Exception inner) : base(message, inner) { }
}
