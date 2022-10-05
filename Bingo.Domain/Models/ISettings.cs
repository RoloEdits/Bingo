namespace Bingo.Domain.Models;

public interface ISettings
{
    public bool WillLogStats { get; }
    public bool WillCountAllSameGuessersInStats { get; }
}
