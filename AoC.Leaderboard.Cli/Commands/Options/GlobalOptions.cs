using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Options;

public class GlobalOptions : CommandSettings
{
    [CommandOption("-e|--eventId")]
    public string EventId { get; set; }

    [CommandOption("-l|--leaderboard")]
    public string Leaderboard { get; set; }

    [CommandOption("--debug")]
    public bool Debug { get; set; }
}

