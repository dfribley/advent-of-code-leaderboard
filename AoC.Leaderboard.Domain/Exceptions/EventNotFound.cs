namespace AoC.Leaderboard.Domain.Exceptions;

public class EventNotFound : Exception
{
    public string EventId { get; init; }
}
