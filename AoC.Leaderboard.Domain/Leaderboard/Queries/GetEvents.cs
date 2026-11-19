using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Leaderboard.Queries;

public class GetEvents : IRequest<IEnumerable<Event>> { }
