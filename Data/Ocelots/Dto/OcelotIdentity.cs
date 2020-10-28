using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Data.Ocelots.Dto
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
