using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.QueryHandlers;

public class GetEventsHandler : IRequestHandler<GetEvents, IEnumerable<Event>>
{
    private readonly IEventService _eventService;

    public GetEventsHandler(IEventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<IEnumerable<Event>> Handle(GetEvents request, CancellationToken cancellationToken)
    {
        return await _eventService.GetEventsAsync();
    }
}
