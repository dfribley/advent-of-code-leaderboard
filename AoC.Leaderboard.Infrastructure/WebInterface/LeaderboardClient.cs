using System.Net.Http.Json;
using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Models;
using AutoMapper;
using HtmlAgilityPack;

namespace AoC.Leaderboard.Infrastructure.WebInterface;

public class LeaderboardClient : ILeaderboardService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;

    public LeaderboardClient(IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
    }

    public async Task<Domain.Models.Leaderboard> GetLeaderboardAsync(string eventId, LeaderboardMembership leaderboard)
    {
        var uri = $"{eventId}/leaderboard/private/view/{leaderboard.Id}.json";
        var httpClient = _httpClientFactory.CreateClient(DependencyInjection.HttpClientName);

        return _mapper.Map<Domain.Models.Leaderboard>(await httpClient.GetFromJsonAsync<Models.Leaderboard>(uri));
    }

    public async Task<IEnumerable<LeaderboardMembership>> GetLeaderboardsAsync()
    {
        var uri = $"leaderboard/private";
        var httpClient = _httpClientFactory.CreateClient(DependencyInjection.HttpClientName);

        var response = await httpClient.GetAsync(uri);

        var html = new HtmlDocument();
        html.LoadHtml(await response.Content.ReadAsStringAsync());

        var leaderboards = new List<LeaderboardMembership>();

        foreach (var viewLink in html.DocumentNode.SelectNodes("//a[. = '[View]']"))
        {
            var boardId = viewLink.Attributes["href"].Value[(viewLink.Attributes["href"].Value.LastIndexOf('/') + 1)..];
            var boardName = "(mine)";

            if (viewLink.ParentNode.Name == "div")
            {
                boardName = viewLink.ParentNode.LastChild.InnerText.Trim();
            }

            leaderboards.Add(new LeaderboardMembership
            {
                Id = boardId,
                Name = boardName
            });
        }

        return leaderboards;
    }
}
