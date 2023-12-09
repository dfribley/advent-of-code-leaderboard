namespace AoC.Leaderboard.Domain.Interfaces;

public interface IEventService
{
    Task<IEnumerable<string>> GetEventsAsync();

    Task<string> GetDayTitle(string eventId, int day);
}
