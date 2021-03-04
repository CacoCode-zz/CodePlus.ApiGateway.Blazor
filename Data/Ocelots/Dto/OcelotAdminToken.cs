using Newtonsoft.Json;

namespace CodePlus.ApiGateway.Data.Ocelots.Dto
{
    public class OcelotAdminToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

    }
}
