using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OddsApiSharp.ClientV2;
using OddsApiSharp.ClientV2.Containers;
using Websocket.Client;

public class OddsApiClient : BackgroundService
{
    private const string BaseUrl = "https://api-v2.oddspapi.io";
    private string _apiToken;
    private string _clientName;
    private string _allowedBookamkers;

    private readonly ILogger<OddsApiClient> _logger;
    private readonly IOptions<OddsApiSettings> _oddsApiSettings;
    private readonly HttpClient _httpClient;
    private readonly WebsocketClient _websocketClient;

    public OddsApiClient(ILogger<OddsApiClient> logger, IOptions<OddsApiSettings> oddsApiSettings)
    {
        _logger = logger;
        _oddsApiSettings = oddsApiSettings;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_oddsApiSettings.Value.ApiUrl);
    }


    #region Implementation of IHostedService

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _websocketClient.Start();

        var updateTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));

        while (await updateTimer.WaitForNextTickAsync(stoppingToken))
        {
            var live = await GetFixturesLive();

            foreach (var liveEventsWrapper in live)
            {
                foreach (var tournament in liveEventsWrapper.Tournaments)
                {
                    foreach (var fixture in tournament.Fixtures)
                    {
                        var odds = await GetOdds(fixture.FixtureId);

                        if (odds == null)
                            continue;

                        if (!_fixtureHandlers.ContainsKey(fixture.FixtureId))
                        {
                            _logger.LogInformation("New fixture: {fixtureId}: {runner1} vs {runner2}",
                                fixture.FixtureId, fixture.Participant1Name, fixture.Participant2Name);

                            _fixtureHandlers[fixture.FixtureId] = new FixtureDataWrapper(odds.Tournaments.First().Fixtures.First());
                        }
                    }
                }
            }
        }
    }

    private async Task<List<LiveEventsWrapper>> GetFixturesLive()
    {
        var response = await _httpClient.GetAsync($"/api/v2/fixtures/live?API-Key={_apiToken}");
        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<LiveEventsWrapper>>(content);
    }

    private async Task<OddsResponseWrapper?> GetOdds(string fixtureId)
    {
        var response = await _httpClient.GetAsync(
            $"/api/v2/odds?fixtureId={fixtureId}&bookmakers={_allowedBookamkers}&API-Key={_apiToken}");
        var content = await response.Content.ReadAsStringAsync();

        if (content.Contains("No fixtures with odds found for the specified fixture and bookmakers."))
        {
            return null;
        }

        return JsonConvert.DeserializeObject<List<OddsResponseWrapper>>(content).First();
    }

    private Dictionary<string, FixtureDataWrapper> _fixtureHandlers = new Dictionary<string, FixtureDataWrapper>();

    #endregion
}