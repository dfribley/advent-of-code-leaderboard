using AoC.Leaderboard.Domain.Models;

namespace AoC.Leaderboard.Domain.Interfaces;

public interface IContextProvider
{
    Task<GlobalContext> GetCurrentContextAsync();

    Task SaveCurrentContextAsync();
}
