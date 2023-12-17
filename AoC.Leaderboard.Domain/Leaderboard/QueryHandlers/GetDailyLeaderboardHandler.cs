using AoC.Leaderboard.Domain.Context.Queries;
using AoC.Leaderboard.Domain.Exceptions;
using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.QueryHandlers;

public class GetDailyLeaderboardHandler : IRequestHandler<GetDailyLeaderboard, DailyLeaderboard>
{
    private readonly IMediator _mediator;
    private readonly ILeaderboardService _leaderboardService;
    private readonly IEventService _eventService;

    public GetDailyLeaderboardHandler(
        IMediator mediator,
        ILeaderboardService leaderboardService,
        IEventService eventService)
    {
        _mediator = mediator;
        _leaderboardService = leaderboardService;
        _eventService = eventService;
    }

    public async Task<DailyLeaderboard> Handle(GetDailyLeaderboard request, CancellationToken cancellationToken)
    {
        var currendContext = await _mediator.Send(new GetContext(), cancellationToken);
        var puzzleStart = new DateTime(Convert.ToInt32(currendContext.EventId), 12, request.Day, 5, 0, 0, DateTimeKind.Utc);

        if (puzzleStart.Year == DateTime.UtcNow.Year && puzzleStart.Day > DateTime.UtcNow.AddHours(-5).Day)
        {
            throw new PuzzleNotAvailable { EventId = currendContext.EventId, Day = puzzleStart.Day };
        }

        var leaderboard = await _leaderboardService.GetLeaderboardAsync(currendContext.EventId, currendContext.Leaderboard);
        var dailyCompletions = new List<DailyCompletion>();

        var completions = leaderboard.Players
            .Where(p => p.Completions.ContainsKey(puzzleStart.Day))
            .Select(p => (Player: p, p.Completions[puzzleStart.Day].Part1, p.Completions[puzzleStart.Day].Part2));

        var points = completions
            .OrderBy(c => c.Part1)
            .Select((c, i) => (c.Player.Id, Points: leaderboard.Players.Count() - i))
            .ToDictionary(t => t.Id, t => t.Points);

        foreach (var (Id, Points) in completions
            .Where(c => c.Part2 != null)
            .OrderBy(c => c.Part2)
            .Select((c, i) => (c.Player.Id, Points: leaderboard.Players.Count() - i)))
        {
            points[Id] += Points;
        }

        dailyCompletions.AddRange(completions
            .Select(c =>
            {
                TimeSpan part1Duration = c.Part1 - puzzleStart;
                TimeSpan? part2Duration = null;
                TimeSpan? puzzleDuration = null;

                if (c.Part2.HasValue)
                {
                    part2Duration = c.Part2.Value - c.Part1;
                    puzzleDuration = part2Duration + part1Duration;
                }

                return new DailyCompletion
                {
                    Player = c.Player,
                    Part1 = c.Part1,
                    Part2 = c.Part2,
                    Part1Duration = part1Duration,
                    Part2Duration = part2Duration,
                    PuzzleDuration = puzzleDuration,
                    Points = points[c.Player.Id]
                };
            })
        );

        return new()
        {
            Day = puzzleStart.Day,
            Title = $"Event {currendContext.EventId} {await _eventService.GetDayTitle(currendContext.EventId, puzzleStart.Day)}",
            Completions = dailyCompletions
        };
    }
}

