using AoC.Leaderboard.Domain.Exceptions;
using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class EventNotSetHandler : IExceptionHandler<EventNotSet>
{
    public void Handle(EventNotSet exception)
    {
        AnsiConsole.MarkupLine("[red]Error:[/] Advent of Code event [yellow1]is not configured[/]. Run the [deepskyblue3_1]set context[/] or the [deepskyblue3_1]set event[/] command to set it.");
    }
}
