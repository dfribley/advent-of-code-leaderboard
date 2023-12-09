namespace AoC.Leaderboard.Domain.Exceptions;

public class LeaderboardNotFound : Exception
{
	public string LeaderboardId { get; init; }
}
