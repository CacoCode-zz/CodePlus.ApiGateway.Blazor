using Newtonsoft.Json;

namespace CodePlus.ApiGateway.Data.Ocelots.Dto
{
    public class OcelotIdentity
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

    }
}
