using System.Text.Json;
using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Models;

namespace AoC.Leaderboard.Infrastructure.FileSystem;

public class GlobalContextProvider : IContextProvider
{
    private readonly string FileName;
    private GlobalContext _currentContext;

    public GlobalContextProvider()
    {
        FileName = $"{Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])}{Path.DirectorySeparatorChar}context.json";
    }

    public async Task<GlobalContext> GetCurrentContextAsync()
    {
        if (_currentContext == null)
        {
            if (File.Exists(FileName))
            {
                using var openStream = File.OpenRead(FileName);
                _currentContext = await JsonSerializer.DeserializeAsync<GlobalContext>(openStream);
            }
            else
            {
                _currentContext = new();
            }
        }

        return _currentContext;
    }

    public async Task SaveCurrentContextAsync()
    {
        if (_currentContext != null)
        {
            using var createStream = File.Create(FileName);
            await JsonSerializer.SerializeAsync(createStream, _currentContext);
            await createStream.DisposeAsync();
        }
    }
}
