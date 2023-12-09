namespace AoC.Leaderboard.Domain.Models;

public class Leaderboard
{
	public Player Owner { get; init; }

	public string Event { get; init; }

	public IEnumerable<Player> Players { get; init; }

}
