using Bingo.Domain.Models;

namespace Bingo.Core;

public class Settings : ISettings
{
    public bool WillLogStats { get; set; } = true;
    public bool WillCountAllSameGuessersInStats { get; set; } = false;
    public bool AllowSkippingWhenThereIsNoBonus { get; } = true;
}
