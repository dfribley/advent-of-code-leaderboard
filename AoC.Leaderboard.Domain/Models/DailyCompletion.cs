namespace AoC.Leaderboard.Domain.Models;

public class DailyCompletion
{
	public Player Player { get; init; }

	public int Points { get; init; }

	public TimeSpan Part1Duration { get; init; }

	public TimeSpan? Part2Duration { get; init; }

	public TimeSpan? PuzzleDuration { get; init; }

    public DateTime Part1 { get; init; }

    public DateTime? Part2 { get; init; }
}
