using AoC.Leaderboard.Domain.Context.Queries;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Show;

public class ShowContextCommand : AsyncCommand
{
    private readonly IMediator _mediator;

    public ShowContextCommand(IMediator mediator)
	{
        _mediator = mediator;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var currentContext = await _mediator.Send(new GetContext());

        var table = new Table()
        {
            Border = TableBorder.Minimal
        };

        table.AddColumns("Event", "Leaderboard");
        table.AddRow(currentContext.EventId, currentContext.Leaderboard.ToString());

        AnsiConsole.Write(table);

        return 0;
    }
}
