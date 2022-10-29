namespace Bingo.Domain.Errors;

[Serializable]
public class GuessedMoreThanOnceException : Exception
{
    public string PlayerName { get; }

    public GuessedMoreThanOnceException()
    {
    }

    public GuessedMoreThanOnceException(string message) : base(message)
    {
    }

    public GuessedMoreThanOnceException(string message, string playerName) : this(message)
    {
        PlayerName = playerName;
    }
}