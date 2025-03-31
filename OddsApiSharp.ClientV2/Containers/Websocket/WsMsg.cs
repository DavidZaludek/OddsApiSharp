using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers.Websocket;

public partial class WsMsg
{
    [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
    public string Channel { get; set; }

    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }

    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public WsFixtureData Data { get; set; }
}