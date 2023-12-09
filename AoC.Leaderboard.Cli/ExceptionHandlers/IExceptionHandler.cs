namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public interface IExceptionHandler<TException> where TException : Exception
{
    void Handle(TException exception);
}
