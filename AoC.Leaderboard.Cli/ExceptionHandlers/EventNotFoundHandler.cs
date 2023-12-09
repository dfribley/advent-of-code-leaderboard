using AoC.Leaderboard.Domain.Exceptions;
using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class EventNotFoundHandler : IExceptionHandler<EventNotFound>
{
    public void Handle(EventNotFound exception)
    {
        AnsiConsole.MarkupLine($"[red]Error:[/] Advent of Code event \"[deepskyblue3_1]{exception.EventId}[/]\" [yellow1]does not exist[/]");
    }
}
