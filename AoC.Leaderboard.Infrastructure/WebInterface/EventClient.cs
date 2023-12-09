using AoC.Leaderboard.Domain.Interfaces;
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

    public async Task<IEnumerable<string>> GetEventsAsync()
    {
        var uri = $"events";
        var httpClient = _httpClientFactory.CreateClient(DependencyInjection.HttpClientName);

        var response = await httpClient.GetAsync(uri);

        var html = new HtmlDocument();
        html.LoadHtml(await response.Content.ReadAsStringAsync());

        var events = new List<string>();

        foreach (var eventDiv in html.DocumentNode.SelectNodes("//div[contains(@class, 'eventlist-event')]"))
        {
            events.Add(new string(eventDiv.FirstChild.InnerText.Where(char.IsDigit).ToArray()));
        }

        return events;
    }
}
