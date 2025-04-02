using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Websocket.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OddsApiSharp.ClientV2.Containers.Websocket;

namespace OddsApiSharp.ClientV2;

public class OddsApiWebsocket : IHostedService
{
    private readonly ILogger<OddsApiWebsocket> _logger;
    private readonly IOptions<OddsApiSettings> _oddsApiSettings;
    private readonly WebsocketClient _websocketClient;

    public event EventHandler<WsFixturesStatus> FixtureStatusUpdated;
    public event EventHandler<WsFixtureData> OddsGroupedUpdate;

    public OddsApiWebsocket(ILogger<OddsApiWebsocket> logger,IOptions<OddsApiSettings> oddsApiSettings)
    {
        _logger = logger;
        _oddsApiSettings = oddsApiSettings;

        _websocketClient = new WebsocketClient(new Uri(_oddsApiSettings.Value.WebsocketUrl));
        _websocketClient.ReconnectionHappened.Subscribe(info =>
        {
            _websocketClient.Send($"subscribe:{_oddsApiSettings.Value.ClientName}:{_oddsApiSettings.Value.ApiKey}");
        });

        _websocketClient.MessageReceived.Subscribe(OnWebsocketMessage);
    }


    private void OnWebsocketMessage(ResponseMessage msg)
    {
        if (msg.Text.Contains("Subscription to ODDS-API by oddspapi.io confirmed for client: "))
            return;
        

        if (msg.Text.StartsWith("Access Details"))
        {
            _logger.LogInformation(msg.Text);
            return;
        }

        var wsMsg = JsonConvert.DeserializeObject<WsMsgHeader>(msg.Text);

        switch (wsMsg.Channel)
        {
            case "oddsGrouped":
                var oddsData = JsonConvert.DeserializeObject<WsMsgOddsGrouped>(msg.Text).Data;
                
                OddsGroupedUpdate?.Invoke(this, oddsData);
                break;
            case "fixtures":
                var fixturesStatus = JsonConvert.DeserializeObject<WsMsgFixtures>(msg.Text).Data;

                FixtureStatusUpdated?.Invoke(this, fixturesStatus);
                break;
            default:
                break;
        }
    }

    #region Implementation of IHostedService

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _websocketClient.Start();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _websocketClient.Stop(WebSocketCloseStatus.NormalClosure, "Stopping hosted service.");
    }

    #endregion
}