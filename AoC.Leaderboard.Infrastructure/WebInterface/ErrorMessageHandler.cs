﻿namespace AoC.Leaderboard.Infrastructure.WebInterface;

class ErrorMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        return response;
    }
}
