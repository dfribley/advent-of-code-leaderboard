using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.QueryHandlers;

public class GetLeaderboardsHandler : IRequestHandler<GetLeaderboards, IEnumerable<LeaderboardMembership>>
{
    private readonly ILeaderboardService _leaderboardService;

    public GetLeaderboardsHandler(ILeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    public async Task<IEnumerable<LeaderboardMembership>> Handle(GetLeaderboards request, CancellationToken cancellationToken)
    {
        return await _leaderboardService.GetLeaderboardsAsync();
    }
}
