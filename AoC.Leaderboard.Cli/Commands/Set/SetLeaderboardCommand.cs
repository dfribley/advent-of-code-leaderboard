using AoC.Leaderboard.Domain.Context.Commands;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Set;

public class SetLeaderboardCommand(IMediator mediator) : AsyncCommand
{
    public override async Task<int> ExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        var leaderboards = await mediator.Send(new GetLeaderboards(), cancellationToken);
        var leaderboard = leaderboards.FirstOrDefault() ?? throw new Exception("Unable to locate a private leaderboard for your account.");

        if (leaderboards.Count() > 1)
        {
            var selectionPrompt = new SelectionPrompt<LeaderboardMembership>()
                .Title("Select leaderboard to view:")
                .AddChoices(leaderboards)
                .HighlightStyle(new Style(Color.Green3))
                .UseConverter(lb => $"{lb.Id}: {lb.Name}");

            leaderboard = AnsiConsole.Prompt(selectionPrompt);
        }
        
        await mediator.Send(new SetLeaderboard { Leaderboard = leaderboard }, cancellationToken);

        return 0;
    }
}
