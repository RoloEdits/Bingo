namespace Bingo.Domain.Models;

public interface ISettings
{
    public bool WillCountAllSameGuessersInStats { get; }
    public bool AllowSkippingWhenThereIsNoBonus { get; }
}