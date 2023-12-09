namespace AoC.Leaderboard.Domain.Models;

public class LeaderboardMembership
{
	public string Id { get; init; }

	public string Name { get; init; }

	public override string ToString() => $"{Id} ({Name})";
}
