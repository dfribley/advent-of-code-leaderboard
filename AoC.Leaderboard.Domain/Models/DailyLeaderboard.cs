namespace AoC.Leaderboard.Domain.Models;

public class DailyLeaderboard
{
	public int Day { get; init; }

	public string Title { get; init; }

	public IEnumerable<DailyCompletion> Completions { get; init; }
}
