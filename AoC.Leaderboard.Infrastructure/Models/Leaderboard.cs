using System.Text.Json.Serialization;

namespace AoC.Leaderboard.Infrastructure.Models;

public class Leaderboard
{
    [JsonPropertyName("owner_id")]
    public int OwnerId { get; init; }

    [JsonPropertyName("event")]
    public string Event { get; init; }

    [JsonPropertyName("members")]
    public IDictionary<string, Member> Members { get; init; }
}
