using System.Text;
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

    public override async Task<int> ExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        var events = await _mediator.Send(new GetEvents(), cancellationToken);
        var totalStars = 0;

        var table = new Table { Border = TableBorder.Minimal };
        table.AddColumns("Event");

        foreach (var e in events)
        {
            var rowText = new StringBuilder(e.Id);
            
            if (e.Stars > 0)
            {
                totalStars += e.Stars;
                rowText.Append($" [yellow1] {e.Stars}*[/]");
            }
            
            table.AddRow(rowText.ToString());
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine($"Total Stars: [yellow1]{totalStars}*[/]");

        return 0;
    }
}
