namespace Bingo.Domain.Errors;

[Serializable]
public class UnhandledException : Exception
{
    public UnhandledException()
    {
    }

    public UnhandledException(string message) : base(message)
    {
    }
}
