using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.Queries;

public class GetEvents : IRequest<IEnumerable<string>> { }
