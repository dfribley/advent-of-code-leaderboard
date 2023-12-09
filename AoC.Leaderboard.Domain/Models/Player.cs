namespace AoC.Leaderboard.Domain.Models;

public class Player
{
	public string Id { get; init; }

	public string Name { get; init; }

	public int LocalScore { get; init; }

	public int GlobalScore { get; init; }

	public int Stars { get; init; }

	public DateTime LastStarTimestamp { get; init; }

	public IDictionary<int, PuzzleCompletion> Completions { get; init; }

	public override string ToString() => Name;
}
