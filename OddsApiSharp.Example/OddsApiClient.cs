using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OddsApiSharp.ClientV2;
using OddsApiSharp.ClientV2.Containers;
using OddsApiSharp.ClientV2.Containers.Websocket;
using Websocket.Client;

public class OddsApiClient : BackgroundService
{
    private readonly ILogger<OddsApiClient> _logger;
    private readonly OddsApiConnection _oddsApiConnection;
    private readonly OddsApiWebsocket _oddsApiWebsocket;

    private Dictionary<string, FixtureDataWrapper> _fixtureHandlers = new();

    public OddsApiClient(ILogger<OddsApiClient> logger, OddsApiConnection oddsApiConnection, OddsApiWebsocket oddsApiWebsocket)
    {
        _logger = logger;
        _oddsApiConnection = oddsApiConnection;
        _oddsApiWebsocket = oddsApiWebsocket;

        _oddsApiWebsocket.FixtureStatusUpdated += OddsApiWebsocketOnFixtureStatusUpdated;
        _oddsApiWebsocket.OddsGroupedUpdate += OddsApiWebsocketOnOddsGroupedUpdate;
    }

    private void OddsApiWebsocketOnOddsGroupedUpdate(object? sender, WsFixtureData e)
    {
        _fixtureHandlers[e.FixtureId].HandleOddsUpdate(e);
    }

    private void OddsApiWebsocketOnFixtureStatusUpdated(object? sender, WsFixturesStatus e)
    {
        _fixtureHandlers[e.FixtureId].HandleStatusUpdate(e);
    }


    #region Implementation of IHostedService

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var updateTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));

        while (await updateTimer.WaitForNextTickAsync(stoppingToken))
        {
            var live = await _oddsApiConnection.GetFixturesLive();

            foreach (var liveEventsWrapper in live)
            {
                foreach (var tournament in liveEventsWrapper.Tournaments)
                {
                    foreach (var fixture in tournament.Fixtures)
                    {
                        try
                        {
                            var odds = await _oddsApiConnection.GetOdds(fixture.FixtureId);

                            if (odds == null)
                                continue;

                            if (!_fixtureHandlers.ContainsKey(fixture.FixtureId))
                            {
                                _logger.LogInformation("New fixture: {fixtureId}: {runner1} vs {runner2}",
                                    fixture.FixtureId, fixture.Participant1Name, fixture.Participant2Name);

                                _fixtureHandlers[fixture.FixtureId] =
                                    new FixtureDataWrapper(odds.Tournaments.First().Fixtures.First());
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.LogError("Failed odds request for {fixtureId}: {runner1} vs {runner2}", fixture.FixtureId, fixture.Participant1Name, fixture.Participant2Name);
                        }
                    }
                }
            }
        }
    }
    

    #endregion
}