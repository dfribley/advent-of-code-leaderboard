using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.Queries;

public class GetDailyLeaderboard : IRequest<DailyLeaderboard>
{
    public int Day { get; init; }
}
