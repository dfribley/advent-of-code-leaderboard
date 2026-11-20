using System.Text.Json;
using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Models;

namespace AoC.Leaderboard.Infrastructure.FileSystem;

public class GlobalContextProvider : IContextProvider
{
    private readonly string _fileName;
    private GlobalContext _currentContext;

    public GlobalContextProvider()
    {
        var userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        if (string.IsNullOrEmpty(userHome))
        {
            throw new InvalidOperationException("Unable to determine user home directory.");
        }
        
        var aocLbDir = Path.Combine(userHome, ".aoclb");
        Directory.CreateDirectory(aocLbDir);

        _fileName = Path.Combine(aocLbDir, "context.json");
    }

    public async Task<GlobalContext> GetCurrentContextAsync()
    {
        if (_currentContext == null)
        {
            if (File.Exists(_fileName))
            {
                await using var openStream = File.OpenRead(_fileName);
                _currentContext = await JsonSerializer.DeserializeAsync<GlobalContext>(openStream);
            }
            else
            {
                _currentContext = new GlobalContext();
            }
        }

        return _currentContext;
    }

    public async Task SaveCurrentContextAsync()
    {
        if (_currentContext != null)
        {
            await using var createStream = File.Create(_fileName);
            await JsonSerializer.SerializeAsync(createStream, _currentContext);
        }
    }
}
