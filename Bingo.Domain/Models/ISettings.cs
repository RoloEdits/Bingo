namespace Bingo.Domain.Models;

public interface ISettings
{
    public bool WillLogStats { get; set; }
    public bool WillCountAllSameGuessersInStats { get; set; }
}