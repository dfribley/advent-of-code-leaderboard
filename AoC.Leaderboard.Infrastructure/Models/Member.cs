using System.Text.Json.Serialization;
using AoC.Leaderboard.Infrastructure.Converters;

namespace AoC.Leaderboard.Infrastructure.Models;

public class Member
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("local_score")]
    public int LocalScore { get; init; }

    [JsonPropertyName("global_score")]
    public int GlobalScore { get; init; }

    [JsonPropertyName("stars")]
    public int Stars { get; init; }

    [JsonPropertyName("last_star_ts")]
    [JsonConverter(typeof(EpochToDateTimeJsonConverter))]
    public DateTime LastStarTimestamp { get; init; }

    [JsonPropertyName("completion_day_level")]
    public IDictionary<string, IDictionary<string, PuzzleCompletion>> DayCompletions { get; init; }
}
