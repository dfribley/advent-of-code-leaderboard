using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Context.Commands;

public class SetLeaderboard : IRequest
{
    public LeaderboardMembership Leaderboard { get; init; }
}
