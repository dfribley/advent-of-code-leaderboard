using AoC.Leaderboard.Domain.Context.Commands;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Set;

public class SetLeaderboardCommand : AsyncCommand
{
    private readonly IMediator _mediator;

    public SetLeaderboardCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var leaderboards = await _mediator.Send(new GetLeaderboards());
        var leaderboard = leaderboards.FirstOrDefault() ?? throw new("Unable to locate a private leaderboard for your account.");

        if (leaderboards.Count() > 1)
        {
            var selectionPrompt = new SelectionPrompt<LeaderboardMembership>()
                .Title("Select leaderboard to view:")
                .AddChoices(leaderboards)
                .HighlightStyle(new Style(Color.Green3))
                .UseConverter(lb => $"{lb.Id}: {lb.Name}");

            leaderboard = AnsiConsole.Prompt(selectionPrompt);
        }
        
        await _mediator.Send(new SetLeaderboard { Leaderboard = leaderboard });

        return 0;
    }
}
