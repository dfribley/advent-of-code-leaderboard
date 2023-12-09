using AoC.Leaderboard.Domain.Context.Queries;
using AoC.Leaderboard.Domain.Interfaces;
using MediatR;

namespace AoC.Leaderboard.Domain.Context.QueryHandlers
{
    public class GetSessionIdHandler : IRequestHandler<GetSessionId, string>
    {
        private readonly ISessionProvider _sessionProvider;

        public GetSessionIdHandler(ISessionProvider sessionProvider)
		{
            _sessionProvider = sessionProvider;
        }

        public Task<string> Handle(GetSessionId request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_sessionProvider.GetSessionToken());
        }
    }
}

