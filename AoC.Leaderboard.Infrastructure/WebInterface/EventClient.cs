using AoC.Leaderboard.Domain.Interfaces;
using AoC.Leaderboard.Domain.Models;
using AoC.Leaderboard.Infrastructure.Extensions;
using HtmlAgilityPack;

namespace AoC.Leaderboard.Infrastructure.WebInterface;

public class EventClient : IEventService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EventClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetDayTitle(string eventId, int day)
    {
        var uri = $"{eventId}/day/{day}";
        var httpClient = _httpClientFactory.CreateClient(DependencyInjection.HttpClientName);

        var response = await httpClient.GetAsync(uri);

        var html = new HtmlDocument();
        html.LoadHtml(await response.Content.ReadAsStringAsync());

        return html.DocumentNode.SelectSingleNode("//h2").InnerText;
    }

    public async Task<IEnumerable<Event>> GetEventsAsync()
    {
        var uri = "events";
        var httpClient = _httpClientFactory.CreateClient(DependencyInjection.HttpClientName);

        var response = await httpClient.GetAsync(uri);

        var html = new HtmlDocument();
        html.LoadHtml(await response.Content.ReadAsStringAsync());

        return html.DocumentNode.SelectNodes("//div[contains(@class, 'eventlist-event')]")
            .Select(eventDiv => new Event
            {
                Id = eventDiv.SelectSingleNode(".//a").InnerText.ExtractInt32().ToString(),
                Stars = eventDiv.SelectSingleNode(".//span")?.InnerText.ExtractInt32() ?? 0
            })
            .ToList();
    }
}
