using AoC.Leaderboard.Domain.Context.Commands;
using AoC.Leaderboard.Domain.Interfaces;
using MediatR;

namespace AoC.Leaderboard.Domain.Context.CommandHandlers;

public class SetLeaderboardHandler : IRequestHandler<SetLeaderboard>
{
    private readonly IContextProvider _contextProvider;

    public SetLeaderboardHandler(IContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public async Task Handle(SetLeaderboard request, CancellationToken cancellationToken)
    {
        var currentContext = await _contextProvider.GetCurrentContextAsync();

        currentContext.Leaderboard = request.Leaderboard;

        await _contextProvider.SaveCurrentContextAsync();
    }
}
