using Bingo.Domain.Models;

namespace Bingo.Library;

public class Settings : ISettings
{
    public bool WillLogStats { get; set; } = true;
    public bool WillCountAllSameGuessersInStats { get; set; } = false;
}
