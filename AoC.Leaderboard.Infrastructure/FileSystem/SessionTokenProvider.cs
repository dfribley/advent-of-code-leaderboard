using System.Security.Cryptography;
using AoC.Leaderboard.Domain.Exceptions;
using AoC.Leaderboard.Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace AoC.Leaderboard.Infrastructure.FileSystem;

public class SessionTokenProvider : ISessionProvider
{
    private readonly string FileName;
    private readonly IDataProtector _protector;
    private string _sessionToken;

    public SessionTokenProvider(IDataProtectionProvider provider)
	{
        FileName = $"{Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])}{Path.DirectorySeparatorChar}session";
        _protector = provider.CreateProtector("AoC.SessionTokenProvider");
    }

    public string GetSessionToken()
    {
        if (!File.Exists(FileName))
        {
            throw new SessionNotSetException();
        }

        _sessionToken ??= File.ReadAllText(FileName);

        try
        {
            return _protector.Unprotect(_sessionToken);
        }
        catch (CryptographicException e)
        {
            throw new SessionNotSetException(e);
        }
    }

    public void SetSessionToken(string sessionToken)
    {
        _sessionToken = _protector.Protect(sessionToken);

        File.WriteAllText(FileName, _sessionToken);
    }
}
