using Bingo.Domain.Models;

namespace Bingo.Core;

public record Settings : ISettings
{
    public bool WillCountAllSameGuessersInStats { get; }
    public bool AllowSkippingWhenThereIsNoBonus { get; }

    public Settings(bool willCountAllSameGuessersInStats, bool allowSkippingWhenThereIsNoBonus = true)
    {
        WillCountAllSameGuessersInStats = willCountAllSameGuessersInStats;
        AllowSkippingWhenThereIsNoBonus = allowSkippingWhenThereIsNoBonus;
    }
}
