namespace AoC.Leaderboard.Domain.Interfaces;

public interface ISessionProvider
{
    string GetSessionToken();

    void SetSessionToken(string sessionToken);
}
