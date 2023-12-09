using AoC.Leaderboard.Domain.Models;

namespace AoC.Leaderboard.Domain.Interfaces;

public interface ILeaderboardService
{
    Task<Models.Leaderboard> GetLeaderboardAsync(string eventId, LeaderboardMembership leaderboardMembership);

    Task<IEnumerable<LeaderboardMembership>> GetLeaderboardsAsync();
}
