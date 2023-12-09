using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Context.Queries;

public class GetContext : IRequest<GlobalContext> { }
