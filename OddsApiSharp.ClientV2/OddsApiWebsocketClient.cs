using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Websocket.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OddsApiSharp.ClientV2.Containers.Websocket;

namespace OddsApiSharp.ClientV2;

public class OddsApiWebsocketClient
{
    private readonly ILogger<OddsApiWebsocketClient> _logger;
    private readonly IOptions<OddsApiSettings> _oddsApiSettings;
    private readonly WebsocketClient _websocketClient;

    public event EventHandler<FixtureDataWrapper> FixtureUpdated;

    public OddsApiWebsocketClient(ILogger<OddsApiWebsocketClient> logger,IOptions<OddsApiSettings> oddsApiSettings)
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
        if (msg.Text == "Subscription to ODDS-API by oddspapi.io confirmed for client: jan-live")
        {
            return;
        }

        if (msg.Text.StartsWith("Access Details"))
        {
            _logger.LogInformation(msg.Text);
            return;
        }

        var wsMsg = JsonConvert.DeserializeObject<WsMsgHeader>(msg.Text);


        switch (wsMsg.Channel)
        {
            case "oddsGrouped":

                break;
            case "fixtures":

                break;
            default:
                break;
        }
    }
}