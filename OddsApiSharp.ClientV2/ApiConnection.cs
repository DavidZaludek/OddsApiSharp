using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OddsApiSharp.ClientV2;
using OddsApiSharp.ClientV2.Containers;

public class ApiConnection
{
    private readonly IOptions<OddsApiSettings> _oddsApiSettings;
    private readonly HttpClient _httpClient;

    public ApiConnection(IOptions<OddsApiSettings> oddsApiSettings)
    {
        _oddsApiSettings = oddsApiSettings;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_oddsApiSettings.Value.ApiUrl)
        };
    }

    private async Task<T?> GetAsync<T>(string endpoint)
    {
        var url = $"{endpoint}{(endpoint.Contains("?") ? "&" : "?")}API-Key={_oddsApiSettings.Value.ApiKey}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public Task<List<Bookmaker>?> GetBookmakers(bool meta = false)
        => GetAsync<List<Bookmaker>>($"/api/v2/bookmakers?meta={meta.ToString().ToLower()}");

    public Task<List<Currency>?> GetCurrencies()
        => GetAsync<List<Currency>>("/api/v2/currencies");

    public Task<List<Sport>?> GetSports(bool hasOdds = false)
        => GetAsync<List<Sport>>($"/api/v2/sports?hasOdds={hasOdds.ToString().ToLower()}");

    public Task<List<Tournament>?> GetTournaments(int? sportId = null, bool hasOdds = false)
    {
        var query = new List<string>();
        if (sportId.HasValue) query.Add($"sportId={sportId.Value}");
        query.Add($"hasOdds={hasOdds.ToString().ToLower()}");
        return GetAsync<List<Tournament>>($"/api/v2/tournaments?{string.Join("&", query)}");
    }

    public Task<List<Market>?> GetMarkets(int? sportId = null)
        => GetAsync<List<Market>>($"/api/v2/markets?{(sportId.HasValue ? $"sportId={sportId}" : "")}");

    public Task<List<object>?> GetLanguages()
        => GetAsync<List<object>>("/api/v2/languages");

    public Task<object?> GetFixturesToday()
        => GetAsync<object>("/api/v2/fixtures/today");

    public Task<OddsResponseWrapper?> GetFixturesLive()
        => GetAsync<OddsResponseWrapper>("/api/v2/fixtures/live");

    public Task<object?> GetFixtures(int? tournamentId = null, int? sportId = null, int? participantId = null, int? playerId = null, string bookmaker = null, bool overlap = true)
    {
        var query = new List<string>();
        if (tournamentId.HasValue) query.Add($"tournamentId={tournamentId}");
        if (sportId.HasValue) query.Add($"sportId={sportId}");
        if (participantId.HasValue) query.Add($"participantId={participantId}");
        if (playerId.HasValue) query.Add($"playerId={playerId}");
        if (!string.IsNullOrEmpty(bookmaker)) query.Add($"bookmaker={bookmaker}");
        query.Add($"overlap={overlap.ToString().ToLower()}");
        return GetAsync<object>($"/api/v2/fixtures?{string.Join("&", query)}");
    }

    public Task<OddsResponseWrapper?> GetEvents(int? tournamentId = null, int? sportId = null, int? participantId = null, int? playerId = null)
    {
        var query = new List<string>();
        if (tournamentId.HasValue) query.Add($"tournamentId={tournamentId}");
        if (sportId.HasValue) query.Add($"sportId={sportId}");
        if (participantId.HasValue) query.Add($"participantId={participantId}");
        if (playerId.HasValue) query.Add($"playerId={playerId}");
        return GetAsync<OddsResponseWrapper>($"/api/v2/events?{string.Join("&", query)}");
    }

    public Task<object?> GetHistoricalEvents(string fixtureId)
        => GetAsync<object>($"/api/v2/historical/events?fixtureId={fixtureId}");

    public Task<object?> GetHistoricalFixtures(int? tournamentId = null, int? participantId = null, int? playerId = null, string fixtureIds = null)
    {
        var query = new List<string>();
        if (tournamentId.HasValue) query.Add($"tournamentId={tournamentId}");
        if (participantId.HasValue) query.Add($"participantId={participantId}");
        if (playerId.HasValue) query.Add($"playerId={playerId}");
        if (!string.IsNullOrEmpty(fixtureIds)) query.Add($"fixtureIds={fixtureIds}");
        return GetAsync<object>($"/api/v2/historical/fixtures?{string.Join("&", query)}");
    }

    public Task<object?> GetHistoricalOdds(string fixtureId, string bookmakers)
        => GetAsync<object>($"/api/v2/historical/odds?fixtureId={fixtureId}&bookmakers={bookmakers}");

    public Task<OddsResponseWrapper?> GetOdds(string fixtureId, string bookmakers)
        => GetAsync<OddsResponseWrapper>($"/api/v2/odds?fixtureId={fixtureId}&bookmakers={bookmakers}");

    public Task<object?> GetOdds3Sec(string sportId, string bookmakers)
        => GetAsync<object>($"/api/v2/odds/3sec?sportId={sportId}&bookmakers={bookmakers}");

    public Task<object?> GetOdds10Sec(string sportId, string bookmakers)
        => GetAsync<object>($"/api/v2/odds/10sec?sportId={sportId}&bookmakers={bookmakers}");

    public Task<object?> GetOdds30Sec(string sportId, string bookmakers)
        => GetAsync<object>($"/api/v2/odds/30sec?sportId={sportId}&bookmakers={bookmakers}");

    public Task<object?> GetOddsTournament(int tournamentId, string bookmaker)
        => GetAsync<object>($"/api/v2/odds/tournament?tournamentId={tournamentId}&bookmaker={bookmaker}");

    public Task<object?> GetTopLiquidityFixtures(string bookmaker = null)
        => GetAsync<object>($"/api/v2/liquidity/fixtures/top100{(string.IsNullOrEmpty(bookmaker) ? "" : $"?bookmaker={bookmaker}")}");

    public Task<object?> GetTopLiquidityMarkets(string sportId, string bookmakers)
        => GetAsync<object>($"/api/v2/liquidity/markets/top100?sportId={sportId}&bookmakers={bookmakers}");

    public Task<object?> GetMapping(string bookmaker, string bookmakerFixtureIds)
        => GetAsync<object>($"/api/v2/mapping?bookmaker={bookmaker}&bookmakerFixtureIds={bookmakerFixtureIds}");
}