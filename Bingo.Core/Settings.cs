using Bingo.Domain.Models;

namespace Bingo.Core;

public record Settings(bool WillCountAllSameGuessersInStats, bool AllowSkippingWhenThereIsNoBonus = true);
