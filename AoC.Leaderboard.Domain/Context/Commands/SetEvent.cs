using MediatR;

namespace AoC.Leaderboard.Domain.Context.Commands;

public class SetEvent : IRequest
{
    public string EventId { get; init; }
}
