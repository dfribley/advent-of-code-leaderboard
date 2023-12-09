using AoC.Leaderboard.Domain.Exceptions;
using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class LeaderboardNotFoundHandler : IExceptionHandler<LeaderboardNotFound>
{
    public void Handle(LeaderboardNotFound exception)
    {
        AnsiConsole.MarkupLine($"[red]Error:[/] Advent of Code private leaderboard \"[deepskyblue3_1]{exception.LeaderboardId}[/]\" [yellow1]does not exist[/]");
    }
}
