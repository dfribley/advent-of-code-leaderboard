using Spectre.Console;

namespace AoC.Leaderboard.Cli.ExceptionHandlers;

public class ApiExceptionHandler : IExceptionHandler<HttpRequestException>
{
    public void Handle(HttpRequestException exception)
    {
        AnsiConsole.MarkupLine("[red]Error:[/] Advent of Code API call [yellow1]failed[/]. If this problem persists, try resetting your session token with the [deepskyblue3_1]set session[/] command.");
    }
}
