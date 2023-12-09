using AoC.Leaderboard.Cli.Commands.Options;
using AoC.Leaderboard.Cli.Commands.Set;
using AoC.Leaderboard.Domain.Context.Queries;
using AoC.Leaderboard.Domain.Exceptions;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Show;

public class ShowLeaderboardCommand : AsyncCommand<GlobalOptions>
{
    private readonly IMediator _mediator;
    private readonly SetContextCommand _setContextCommand;

    public ShowLeaderboardCommand(IMediator mediator, SetContextCommand setContextCommand)
    {
        _mediator = mediator;
        _setContextCommand = setContextCommand;
    }

    public async override Task<int> ExecuteAsync(CommandContext context, GlobalOptions settings)
    {
        try
        {
            await _mediator.Send(new GetContext());
        }
        catch (Exception e) when (e is EventNotSet || e is LeaderboardNotSet)
        {
            await _setContextCommand.ExecuteAsync(context);
        }

        var leaderboard = await _mediator.Send(new GetLeaderboard());

        var table = new Table() { Border = TableBorder.Minimal };
        table.AddColumns("Place", "Score", "Stars", "Player");

        var rows = leaderboard.Players
            .OrderByDescending(p => p.LocalScore)
            .ToArray();

        for (int i = 0, score = 0; i < rows.Length; score = rows[i].LocalScore, i++)
        {
            var stars = string.Empty;

            foreach (var day in Enumerable.Range(1, 25))
            {
                var color = "grey7";

                if (rows[i].Completions.TryGetValue(day, out var completion))
                {
                    color = completion.Part2 != null
                        ? color = "yellow"
                        : color = "aqua";
                }

                stars += $"[{color}]*[/]";
            }

            table.AddRow(
                rows[i].LocalScore != score ? (i + 1).ToString() : "",
                rows[i].LocalScore.ToString(),
                stars,
                rows[i].Name
            );
        }

        AnsiConsole.Write(new Padder(new Text($"-- {leaderboard.Event} Leaderboard ({leaderboard.Owner.Name}) --")));
        AnsiConsole.Write(table);

        return await Task.FromResult(0);
    }
}
