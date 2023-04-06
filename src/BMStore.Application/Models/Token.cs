using Newtonsoft.Json;

namespace BMStore.Application.Models;

[JsonObject("Token")]
public class Token
{
    [JsonProperty("Secret")]
    public string Secret { get; set; }

    [JsonProperty("Issuer")]
    public string Issuer { get; set; }

    [JsonProperty("Audience")]
    public string Audience { get; set; }

    [JsonProperty("Expiry")]
    public int Expiry { get; set; }

    [JsonProperty("RefreshExpiry")]
    public int RefreshExpiry { get; set; }
}
