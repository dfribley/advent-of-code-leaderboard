using AoC.Leaderboard.Cli.Commands.Set;
using AoC.Leaderboard.Cli.Commands.Show;
using AoC.Leaderboard.Cli.DependencyInjection;
using AoC.Leaderboard.Cli.ExceptionHandlers;
using AoC.Leaderboard.Cli.Interceptors;
using AoC.Leaderboard.Domain.Exceptions;
using AoC.Leaderboard.Domain.Leaderboard.QueryHandlers;
using AoC.Leaderboard.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var services = new ServiceCollection();

services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<GetLeaderboardHandler>();
});

services
    .UseAoCWebApi()
    .UseFileSystemPersistence();

var app = new CommandApp(new TypeRegistrar(services));
app.Configure(config =>
{
    config.AddBranch("show", show =>
    {
        show.AddCommand<ShowLeaderboardsCommand>("leaderboards");
        show.AddCommand<ShowEventsCommand>("events");
        show.AddCommand<ShowLeaderboardCommand>("leaderboard");
        show.AddCommand<ShowDayCommand>("day");
        show.AddCommand<ShowContextCommand>("context");
    });

    config.AddBranch("set", set =>
    {
        set.AddCommand<SetLeaderboardCommand>("leaderboard");
        set.AddCommand<SetEventCommand>("event");
        set.AddCommand<SetContextCommand>("context");
        set.AddCommand<SetSessionCommand>("session");
    });

    var exceptionHandler = new ExceptionHandler();

    exceptionHandler.RegisterHandler<HttpRequestException, ApiExceptionHandler>();
    exceptionHandler.RegisterHandler<EventNotFound, EventNotFoundHandler>();
    exceptionHandler.RegisterHandler<EventNotSet, EventNotSetHandler>();
    exceptionHandler.RegisterHandler<LeaderboardNotFound, LeaderboardNotFoundHandler>();
    exceptionHandler.RegisterHandler<LeaderboardNotSet, LeaderboardNotSetHandler>();
    exceptionHandler.RegisterHandler<SessionNotSetException, SessionNotSetHandler>();
    exceptionHandler.RegisterHandler<PuzzleNotAvailable, PuzzleNotAvailableHandler>();

    config.SetExceptionHandler(exceptionHandler.Handle);
    config.SetInterceptor(new GlobalOptionsInterceptor());
});

return await app.RunAsync(args);
