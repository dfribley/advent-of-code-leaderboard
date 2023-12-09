using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Infrastructure.FileSystem;
using AoC.Leaderboard.Infrastructure.MapperProfiles;
using AoC.Leaderboard.Infrastructure.WebInterface;
using Microsoft.Extensions.DependencyInjection;

namespace AoC.Leaderboard.Infrastructure;

public static class DependencyInjection
{
    public const string HttpClientName = "AoCWeb";

    public static ServiceCollection UseAoCWebApi(this ServiceCollection services)
    {
        services.AddSingleton<ErrorMessageHandler>();
        services.AddSingleton<ILeaderboardService, LeaderboardClient>();
        services.AddSingleton<IEventService, EventClient>();

        services.AddHttpClient(HttpClientName, (sp, httpClient) =>
        {
            var sessionProvider = sp.GetRequiredService<ISessionProvider>();

            httpClient.BaseAddress = new Uri("https://adventofcode.com/");
            httpClient.DefaultRequestHeaders.Add("Cookie", $"session={sessionProvider.GetSessionToken()}");
        })
        .AddHttpMessageHandler<ErrorMessageHandler>();

        services.AddAutoMapper(typeof(LeaderboardProfiles));

        return services;
    }

    public static ServiceCollection UseFileSystemPersistence(this ServiceCollection services)
    {
        services.AddSingleton<IContextProvider, GlobalContextProvider>();
        services.AddSingleton<ISessionProvider, SessionTokenProvider>();
        services.AddDataProtection();

        return services;
    }
}
