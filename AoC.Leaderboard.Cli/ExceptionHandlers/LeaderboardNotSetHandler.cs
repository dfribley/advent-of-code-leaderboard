using AoC.Leaderboard.Domain.Exceptions;
using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class LeaderboardNotSetHandler : IExceptionHandler<LeaderboardNotSet>
{
    public void Handle(LeaderboardNotSet exception)
    {
        AnsiConsole.MarkupLine("[red]Error:[/] Advent of Code private leaderboard [yellow1]is not configured[/]. Run the [deepskyblue3_1]set context[/] or the [deepskyblue3_1]set leaderboard[/] command to set it.");
    }
}
