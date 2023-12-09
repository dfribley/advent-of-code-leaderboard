using AoC.Leaderboard.Domain.Models;
using AutoMapper;

namespace AoC.Leaderboard.Infrastructure.MapperProfiles;

public class LeaderboardProfiles : Profile
{
	public LeaderboardProfiles()
	{
		CreateMap<Models.Member, Player>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name ?? "Anonymous"))
            .ForMember(d => d.Completions, opt => opt.MapFrom(s =>
                s.DayCompletions.ToDictionary(d => d.Key, d => new PuzzleCompletion
                {
                    Part1 = d.Value["1"].GetStarTimestamp,
                    Part2 = d.Value.ContainsKey("2") ? d.Value["2"].GetStarTimestamp : null
                })));

        CreateMap<Models.Leaderboard, Domain.Models.Leaderboard>()
            .ForMember(d => d.Players, opt => opt.MapFrom(s => s.Members.Values))
            .ForMember(d => d.Owner, opt => opt.MapFrom(s => s.Members[s.OwnerId.ToString()]));
    }
}
