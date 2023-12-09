using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.Queries;

public class GetLeaderboards : IRequest<IEnumerable<LeaderboardMembership>> { }
