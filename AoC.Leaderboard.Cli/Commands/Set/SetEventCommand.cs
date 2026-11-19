using AoC.Leaderboard.Domain.Context.Commands;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Set;

public class SetEventCommand : AsyncCommand
{
    private readonly IMediator _mediator;

    public SetEventCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        var events = await _mediator.Send(new GetEvents(), cancellationToken);
        
        var selectionPrompt = new SelectionPrompt<Event>()
            .Title("What event would you like to view?")
            .AddChoices(events)
            .HighlightStyle(new Style(Color.Green3))
            .UseConverter(e => e.Id);
        var selectedEvent = AnsiConsole.Prompt(selectionPrompt);

        await _mediator.Send(new SetEvent { EventId = selectedEvent.Id }, cancellationToken);

        return 0;
    }
}
