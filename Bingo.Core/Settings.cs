using Bingo.Domain.Models;

namespace Bingo.Core;

public class Settings : ISettings
{
    public bool WillCountAllSameGuessersInStats { get; set; }
    public bool AllowSkippingWhenThereIsNoBonus { get; set; } = true;
}
