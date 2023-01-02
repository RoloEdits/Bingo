namespace Bingo.Domain.Errors;

[Serializable]
public class CannotOpenFileException : Exception
{
    public CannotOpenFileException()
    {
    }

    public CannotOpenFileException(string message) : base(message)
    {
    }

    public CannotOpenFileException(string message, Exception inner) : base(message, inner)
    {
    }
}
