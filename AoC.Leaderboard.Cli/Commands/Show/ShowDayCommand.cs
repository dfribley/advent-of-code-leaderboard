using AoC.Leaderboard.Cli.Commands.Options;
using AoC.Leaderboard.Cli.Commands.Set;
using AoC.Leaderboard.Domain.Context.Queries;
using AoC.Leaderboard.Domain.Exceptions;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;

namespace AoC.Leaderboard.Cli.Commands.Show;

public class ShowDayCommand : AsyncCommand<ShowDayCommand.Settings>
{
    public class Settings : GlobalOptions
    {
        [CommandArgument(0, "[Day]")]
        public int Day { get; set; }

        [CommandOption("--timeline")]
        public bool ShowTimeline { get; set; }

        public override ValidationResult Validate()
        {
            if (Day <= 0)
            {
                Day += DateTime.UtcNow.AddHours(-5).Day;
            }

            return 1 <= Day && Day <= 25
                ? ValidationResult.Success()
                : ValidationResult.Error("Day must be between 1 and 25");
        }
    }

    private readonly IMediator _mediator;
    private readonly SetContextCommand _setContextCommand;

    public ShowDayCommand(IMediator mediator, SetContextCommand setContextCommand)
    {
        _mediator = mediator;
        _setContextCommand = setContextCommand;
    }

    public async override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        try
        {
            await _mediator.Send(new GetContext());
        }
        catch (Exception e) when (e is EventNotSet || e is LeaderboardNotSet)
        {
            await _setContextCommand.ExecuteAsync(context);
        }

        var dailyLeaderboard = await _mediator.Send(new GetDailyLeaderboard { Day = settings.Day });

        AnsiConsole.Write(new Padder(new Text(dailyLeaderboard.Title)));
        AnsiConsole.Write(settings.ShowTimeline ? RenderTimeline(dailyLeaderboard) : RenderBoard(dailyLeaderboard));

        return 0;
    }

    private static IRenderable RenderBoard(DailyLeaderboard board)
    {
        var table = new Table()
        {
            Border = TableBorder.Minimal
        };

        table.AddColumn(new("Place") { Alignment = Justify.Center });
        table.AddColumn("Player");
        table.AddColumn(new("Part 1 Duration") { Alignment = Justify.Right, Padding = new Padding(2) });
        table.AddColumn(new("Part 2 Duration") { Alignment = Justify.Right, Padding = new Padding(2) });
        table.AddColumn(new("Puzzle Duration") { Alignment = Justify.Right, Padding = new Padding(2) });
        table.AddColumn(new("Points") { Alignment = Justify.Center });

        var rows = board.Completions
            .OrderByDescending(c => c.Points)
            .ThenBy(c => c.PuzzleDuration)
            .ToArray();

        for (int i = 0, score = 0; i < rows.Length; score = rows[i].Points, i++)
        {
            table.AddRow(
                rows[i].Points != score ? (i + 1).ToString() : "",
                rows[i].Player.Name,
                rows[i].Part1Duration.ToString("g"),
                rows[i].Part2Duration?.ToString("g") ?? "",
                rows[i].PuzzleDuration?.ToString("g") ?? "",
                rows[i].Points.ToString()
            );
        }

        return table;
    }

    private static IRenderable RenderTimeline(DailyLeaderboard board)
    {
        var timeline = board.Completions
            .Select(c => (timestamp: c.Part1Duration, player: c.Player.Name, part: 1))
            .Concat(board.Completions
                .Where(c => c.PuzzleDuration != null)
                .Select(c => (timestamp: c.PuzzleDuration.Value, player: c.Player.Name, part: 2)))
            .OrderBy(t => t.timestamp);

        var table = new Table { Border = TableBorder.Minimal };

        table.AddColumn(new("Time") { Alignment = Justify.Right, Padding = new Padding(2) });
        table.AddColumn(new("Player") { Padding = new Padding(2) });
        table.AddColumn(new("Part") { Alignment = Justify.Center });

        foreach (var (timestamp, player, part) in timeline)
        {
            table.AddRow(timestamp.ToString("g"), player, $"[{(part == 1 ? "aqua" : "yellow")}]*[/]");
        }

        return table;
    }
}
