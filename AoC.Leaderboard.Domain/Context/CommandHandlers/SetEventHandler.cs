using AoC.Leaderboard.Domain.Context.Commands;
using AoC.Leaderboard.Domain.Interfaces;
using MediatR;

namespace AoC.Leaderboard.Domain.Context.CommandHandlers;

public class SetEventHandler : IRequestHandler<SetEvent>
{
    private readonly IContextProvider _contextProvider;

    public SetEventHandler(IContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public async Task Handle(SetEvent request, CancellationToken cancellationToken)
    {
        var currentContext = await _contextProvider.GetCurrentContextAsync();

        currentContext.EventId = request.EventId;

        await _contextProvider.SaveCurrentContextAsync();
    }
}
