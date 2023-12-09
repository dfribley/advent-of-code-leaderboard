using AoC.Leaderboard.Domain.Context.Queries;
using AoC.Leaderboard.Domain.Exceptions;
using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Leaderboard.Queries;
using AoC.Leaderboard.Domain.Models;
using MediatR;

namespace AoC.Leaderboard.Domain.Context.QueryHandlers
{
    public class GetContextHandler : IRequestHandler<GetContext, GlobalContext>
    {
        private readonly IContextProvider _contextProvider;
        private readonly IMediator _mediator;

        public GetContextHandler(IContextProvider contextProvider, IMediator mediator)
        {
            _contextProvider = contextProvider;
            _mediator = mediator;
        }

        public async Task<GlobalContext> Handle(GetContext request, CancellationToken cancellationToken)
        {
            var currentContext = await _contextProvider.GetCurrentContextAsync();

            if (!string.IsNullOrEmpty(GlobalContext.LeaderboardOverride) && GlobalContext.LeaderboardOverride != currentContext.Leaderboard?.Id)
            {
                var leaderboards = await _mediator.Send(new GetLeaderboards(), cancellationToken);
                currentContext.Leaderboard = leaderboards
                    .FirstOrDefault(m => m.Id == GlobalContext.LeaderboardOverride || m.Name.Contains(GlobalContext.LeaderboardOverride))
                    ?? throw new LeaderboardNotFound { LeaderboardId = GlobalContext.LeaderboardOverride };
            }

            if (!string.IsNullOrEmpty(GlobalContext.EventOverride) && GlobalContext.EventOverride != currentContext.EventId)
            {
                var events = await _mediator.Send(new GetEvents(), cancellationToken);
                currentContext.EventId = events
                    .SingleOrDefault(e => e == GlobalContext.EventOverride)
                    ?? throw new EventNotFound { EventId = GlobalContext.EventOverride };
            }

            if (currentContext.Leaderboard == null)
            {
                var leaderboards = await _mediator.Send(new GetLeaderboards(), cancellationToken);

                try
                {
                    currentContext.Leaderboard = leaderboards.Single();
                    await _contextProvider.SaveCurrentContextAsync();
                }
                catch (InvalidOperationException)
                {
                    throw new LeaderboardNotSet();
                }
            }

            if (string.IsNullOrEmpty(currentContext.EventId))
            {
                throw new EventNotSet();
            }

            return currentContext;
        }
    }
}

