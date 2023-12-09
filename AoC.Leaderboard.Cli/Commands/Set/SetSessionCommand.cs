using AoC.Leaderboard.Domain.Context.Commands;
using MediatR;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Set;

public class SetSessionCommand : AsyncCommand<SetSessionCommand.Settings>
{
    private readonly IMediator _mediator;

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[SessionToken]")]
        public string SessionToken { get; set; }
    }

    public SetSessionCommand(IMediator mediator)
	{
        _mediator = mediator;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        settings.SessionToken ??= AnsiConsole.Prompt(
            new TextPrompt<string>("Enter Advent of Code [green]Session Token[/]:")
                .PromptStyle(Color.DeepSkyBlue3_1)
                .Secret());

        await _mediator.Send(new SetSession { SessionToken = settings.SessionToken });

        return 0;
    }
}
