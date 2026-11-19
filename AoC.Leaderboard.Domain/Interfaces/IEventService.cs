using AoC.Leaderboard.Domain.Models;

namespace AoC.Leaderboard.Domain.Interfaces;

public interface IEventService
{
    Task<IEnumerable<Event>> GetEventsAsync();

    Task<string> GetDayTitle(string eventId, int day);
}
