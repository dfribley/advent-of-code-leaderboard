using AoC.Leaderboard.Domain.Exceptions;
using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class SessionNotSetHandler : IExceptionHandler<SessionNotSetException>
{
    public void Handle(SessionNotSetException exception)
    {
        AnsiConsole.MarkupLine("[red]Error:[/] Advent of Code session token is [yellow1]not configured[/]. Run the [deepskyblue3_1]set session[/] command to set it.");
    }
}
