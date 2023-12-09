using AoC.Leaderboard.Domain.Leaderboard.Queries;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Show;

public class ShowLeaderboardsCommand : AsyncCommand
{
    private readonly IMediator _mediator;

    public ShowLeaderboardsCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async override Task<int> ExecuteAsync(CommandContext context)
    {
        var leaderboards = await _mediator.Send(new GetLeaderboards());

        var table = new Table() { Border = TableBorder.Minimal };

        table.AddColumns("Id", "Name");

        foreach (var leaderboard in leaderboards)
        {
            table.AddRow(leaderboard.Id, leaderboard.Name);
        }

        AnsiConsole.Write(table);

        return 0;
    }
}
