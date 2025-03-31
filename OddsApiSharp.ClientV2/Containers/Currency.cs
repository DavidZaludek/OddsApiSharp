using Newtonsoft.Json;

namespace OddsApiSharp.ClientV2.Containers;

public class Currency
{
    [JsonProperty("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonProperty("currencyName")]
    public string CurrencyName { get; set; }
}