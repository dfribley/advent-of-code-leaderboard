namespace AoC.Leaderboard.Domain.Exceptions;

public class PuzzleNotAvailable : Exception
{
	public string EventId { get; init; }

    public int Day { get; init; }
}
