using System.Text.Json.Serialization;
using AoC.Leaderboard.Infrastructure.Converters;

namespace AoC.Leaderboard.Infrastructure.Models;

public class PuzzleCompletion
{
    [JsonPropertyName("star_index")]
    public int StarIndex { get; init; }

    [JsonPropertyName("get_star_ts")]
    [JsonConverter(typeof(EpochToDateTimeJsonConverter))]
    public DateTime GetStarTimestamp { get; init; }
}
