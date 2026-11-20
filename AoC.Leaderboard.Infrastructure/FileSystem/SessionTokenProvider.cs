using System.Security.Cryptography;
using AoC.Leaderboard.Domain.Exceptions;
using AoC.Leaderboard.Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace AoC.Leaderboard.Infrastructure.FileSystem;

public class SessionTokenProvider : ISessionProvider
{
    private readonly string _fileName;
    private readonly IDataProtector _protector;
    private string _sessionToken;

    public SessionTokenProvider(IDataProtectionProvider provider)
	{
        _fileName = Path.Combine(FileSystemHelper.GetDataDirectory(), "session");
        _protector = provider.CreateProtector("AoC.SessionTokenProvider");
    }

    public string GetSessionToken()
    {
        if (!File.Exists(_fileName))
        {
            throw new SessionNotSetException();
        }

        _sessionToken ??= File.ReadAllText(_fileName);

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

        File.WriteAllText(_fileName, _sessionToken);
    }
}
