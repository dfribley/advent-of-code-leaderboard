using AoC.Leaderboard.Domain.Context.Commands;
using AoC.Leaderboard.Domain.Interfaces;
using MediatR;

namespace AoC.Leaderboard.Domain.Context.CommandHandlers;

public class SetSessionHandler : IRequestHandler<SetSession>
{
    private readonly ISessionProvider _sessionProvider;

    public SetSessionHandler(ISessionProvider sessionProvider)
	{
        _sessionProvider = sessionProvider;
    }

    public Task Handle(SetSession request, CancellationToken cancellationToken)
    {
        _sessionProvider.SetSessionToken(request.SessionToken);

        return Task.CompletedTask;
    }
}
