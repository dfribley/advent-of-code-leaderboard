using AoC.Leaderboard.Cli.Commands.Options;
using AoC.Leaderboard.Domain.Models;
using Spectre.Console.Cli;

namespace AoC.Leaderboard.Cli.Interceptors;

public class GlobalOptionsInterceptor : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is GlobalOptions globalOptions)
        {
            if (!string.IsNullOrEmpty(globalOptions.EventId))
            {
                GlobalContext.EventOverride = globalOptions.EventId;
            }

            if (!string.IsNullOrEmpty(globalOptions.Leaderboard))
            {
                GlobalContext.LeaderboardOverride = globalOptions.Leaderboard;
            }

            GlobalContext.Debug = globalOptions.Debug;
        }
    }
}
