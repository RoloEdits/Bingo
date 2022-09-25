namespace Bingo.Domain.Errors;

[Serializable]
public class NoPlayersException : Exception
{

    public NoPlayersException()
    {
    }

    public NoPlayersException(string message) : base(message)
    {
    }
}