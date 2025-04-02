using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers.Websocket;

public partial class WsMsgOddsGrouped
{
    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public WsFixtureData Data { get; set; }
}

public class WsMsgFixtures
{
    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public WsFixturesStatus Data { get; set; }
}

public partial class WsFixturesStatus
{
    [JsonProperty("fixtureId", NullValueHandling = NullValueHandling.Ignore)]
    public string FixtureId { get; set; }

    [JsonProperty("statusId", NullValueHandling = NullValueHandling.Ignore)]
    public long? StatusId { get; set; }
}