namespace AoC.Leaderboard.Domain.Exceptions;

public class SessionNotSetException : Exception
{
    public SessionNotSetException() { }

    public SessionNotSetException(Exception e) : base(null, e) { }
}
