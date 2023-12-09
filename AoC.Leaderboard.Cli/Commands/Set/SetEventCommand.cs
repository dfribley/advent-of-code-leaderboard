using AoC.Leaderboard.Domain.Context.Commands;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
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

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var selectionPrompt = new SelectionPrompt<string>()
            .Title("What event would you like to view?")
            .AddChoices(await _mediator.Send(new GetEvents()))
            .HighlightStyle(new Style(Color.Green3));
        var eventId = AnsiConsole.Prompt(selectionPrompt);

        await _mediator.Send(new SetEvent { EventId = eventId });

        return 0;
    }
}
