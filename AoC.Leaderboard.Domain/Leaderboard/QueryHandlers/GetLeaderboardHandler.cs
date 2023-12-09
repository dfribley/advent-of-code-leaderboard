using AoC.Leaderboard.Domain.Context.Queries;
using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.QueryHandlers;

public class GetLeaderboardHandler : IRequestHandler<GetLeaderboard, Models.Leaderboard>
{
    private readonly IMediator _mediator;
    private readonly ILeaderboardService _leaderboardService;

    public GetLeaderboardHandler(IMediator mediator, ILeaderboardService leaderboardService)
    {
        _mediator = mediator;
        _leaderboardService = leaderboardService;
    }

    public async Task<Models.Leaderboard> Handle(GetLeaderboard request, CancellationToken cancellationToken)
    {
        var currentContext = await _mediator.Send(new GetContext());

        return await _leaderboardService.GetLeaderboardAsync(currentContext.EventId, currentContext.Leaderboard);
    }
}

