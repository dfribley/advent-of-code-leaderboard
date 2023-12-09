using AoC.Leaderboard.Domain.Context.Queries;
using AoC.Leaderboard.Domain.Exceptions;
using MediatR;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Commands.Set;

public class SetContextCommand : AsyncCommand
{
    private readonly IMediator _mediator;
    private readonly SetLeaderboardCommand _setLeaderboardCommand;
    private readonly SetEventCommand _setEventCommand;
    private readonly SetSessionCommand _setSessionCommand;

    public SetContextCommand(
        IMediator mediator,
        SetLeaderboardCommand setLeaderboardCommand,
        SetEventCommand setEventCommand,
        SetSessionCommand setSessionCommand)
    {
        _mediator = mediator;
        _setLeaderboardCommand = setLeaderboardCommand;
        _setEventCommand = setEventCommand;
        _setSessionCommand = setSessionCommand;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        try
        {
            await _mediator.Send(new GetSessionId());
        }
        catch (SessionNotSetException)
        {
            await _setSessionCommand.ExecuteAsync(context, new SetSessionCommand.Settings());
        }

        await _setEventCommand.ExecuteAsync(context);
        await _setLeaderboardCommand.ExecuteAsync(context);

        return 0;
    }
}

