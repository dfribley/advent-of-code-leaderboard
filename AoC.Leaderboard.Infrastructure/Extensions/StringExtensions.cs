using System.Text.RegularExpressions;

namespace AoC.Leaderboard.Infrastructure.Extensions;

public static class StringExtensions
{
    public static int ExtractInt32(this string input)
    {
        var match = Regex.Match(input, @"\d+");
        return int.Parse(match.Value);
    }
}