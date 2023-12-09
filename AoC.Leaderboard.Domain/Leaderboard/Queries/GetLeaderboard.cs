using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.Queries;

public class GetLeaderboard : IRequest<Models.Leaderboard> { }
