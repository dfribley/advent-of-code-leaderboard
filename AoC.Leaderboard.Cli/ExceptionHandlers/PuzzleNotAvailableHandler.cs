using AoC.Leaderboard.Domain.Exceptions;
using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class PuzzleNotAvailableHandler : IExceptionHandler<PuzzleNotAvailable>
{
    public void Handle(PuzzleNotAvailable exception)
    {
        AnsiConsole.MarkupLine($"[red]Error:[/] Advent of Code {exception.EventId} day {exception.Day} [yellow1]is not available yet[/].");
    }
}
