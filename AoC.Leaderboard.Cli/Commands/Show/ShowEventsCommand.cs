using AoC.Leaderboard.Domain.Leaderboard.Queries;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Show;

public class ShowEventsCommand : AsyncCommand
{
    private readonly IMediator _mediator;

    public ShowEventsCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var events = await _mediator.Send(new GetEvents());

        var table = new Table { Border = TableBorder.Minimal };
        table.AddColumns("Event");

        foreach (var e in events)
        {
            table.AddRow(e);
        }

        AnsiConsole.Write(table);

        return 0;
    }
}
