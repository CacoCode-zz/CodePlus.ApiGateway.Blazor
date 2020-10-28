using CodePlus.Blazor.Data.Https;
using CodePlus.Blazor.Data.Ocelots.Dto;
using Ocelot.Configuration.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodePlus.Blazor.Data.Ocelots
{
    public class OcelotAdminService : IOcelotAdminService
    {
        private readonly IHttpUtilityService _httpUtilityService;

        public OcelotAdminService(IHttpUtilityService httpUtilityService)
        {
            _httpUtilityService = httpUtilityService;
        }

        public async Task<FileConfiguration> GetConfig()
        {
            var tokenInfo = await GetToken();
            var data = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", tokenInfo?.AccessToken);
            return data;
        }

        public async Task<FileConfiguration> SetConfig(string routeConfig)
        {
            var routeObject = JsonConvert.DeserializeObject<FileRoute>(routeConfig);
            var oldConfig = await GetConfig();
            oldConfig.Routes.Add(routeObject);
            var result = await _httpUtilityService.PostAsync<FileConfiguration>("/administration/configuration", oldConfig);
            return result;
        }

        private async Task<OcelotAdminToken> GetToken()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", "admin");
            dic.Add("client_secret", "secret");
            dic.Add("scope", "admin");
            dic.Add("grant_type", "client_credentials");
            var result = await _httpUtilityService.PostFormDataAsync<OcelotAdminToken>("/administration/connect/token", dic);
            return result;
        }
    }
}
