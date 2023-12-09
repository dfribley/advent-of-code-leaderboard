namespace AoC.Leaderboard.Domain.Models;

public class GlobalContext
{
    public static string EventOverride { get; set; }

    public static string LeaderboardOverride { get; set; }

    public static bool Debug { get; set; }

    public string EventId { get; set; }

    public LeaderboardMembership Leaderboard { get; set; }
}