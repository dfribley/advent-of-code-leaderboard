using MediatR;

namespace AoC.Leaderboard.Domain.Context.Commands;

public class SetSession : IRequest
{
    public string SessionToken { get; init; }
}
