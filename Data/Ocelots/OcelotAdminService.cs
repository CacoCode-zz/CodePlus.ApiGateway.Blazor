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

        public async Task<FileConfiguration> GetConfigAsync()
        {
            var tokenInfo = await GetTokenAsync();
            var data = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", tokenInfo?.AccessToken);
            return data;
        }

        public async Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(string routeConfig)
        {
            var routeObject = JsonConvert.DeserializeObject<FileRoute>(routeConfig);
            var tokenInfo = await GetTokenAsync();
            var oldConfig = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", tokenInfo?.AccessToken);
            oldConfig.Routes.Add(routeObject);
            var result = await _httpUtilityService.PostAsync<FileConfiguration>("/administration/configuration", oldConfig, tokenInfo?.AccessToken);
            return result;
        }

        public async Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(FileRoute route)
        {
            var tokenInfo = await GetTokenAsync();
            var oldConfig = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", tokenInfo?.AccessToken);
            oldConfig.Routes.Add(route);
            var result = await _httpUtilityService.PostAsync<FileConfiguration>("/administration/configuration", oldConfig, tokenInfo?.AccessToken);
            if (!string.IsNullOrWhiteSpace(result.HttpResponseMessages))
            {
                oldConfig.Routes.Remove(oldConfig.Routes.Last());
                await _httpUtilityService.PostAsync<FileConfiguration>("/administration/configuration", oldConfig, tokenInfo?.AccessToken);
            }
            return result;
        }

        public async Task<HttpResponseResult<FileConfiguration>> UpdateConfigAsync(FileRoute fileRoute)
        {
            var tokenInfo = await GetTokenAsync();
            var data = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", tokenInfo?.AccessToken);
            for (int i = 0; i < data.Routes.Count; i++)
            {
                var item = data.Routes[i];
                if (item.UpstreamPathTemplate == fileRoute.UpstreamPathTemplate &&
                                    item.DownstreamPathTemplate == fileRoute.DownstreamPathTemplate)
                {
                    data.Routes[i] = fileRoute;
                }
            }
            var result = await _httpUtilityService.PostAsync<FileConfiguration>("/administration/configuration", data, tokenInfo?.AccessToken);
            return result;
        }

        public async Task<HttpResponseResult<FileConfiguration>> DeleteConfigAsync(string upUrl, string downUrl)
        {
            var tokenInfo = await GetTokenAsync();
            var data = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", tokenInfo?.AccessToken);
            var route = data.Routes.FirstOrDefault(a => a.UpstreamPathTemplate == upUrl && a.DownstreamPathTemplate == downUrl);
            if (route == null)
            {
                throw new KeyNotFoundException("路由映射不存在");
            }
            data.Routes.Remove(route);
            var result = await _httpUtilityService.PostAsync<FileConfiguration>("/administration/configuration", data, tokenInfo?.AccessToken);
            return result;
        }


        public async Task<HttpResponseResult<FileRoute>> GetConfigByAsync(string upUrl,string downUrl)
        {
            var tokenInfo = await GetTokenAsync();
            var data = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", tokenInfo?.AccessToken);
            var route = data.Routes.FirstOrDefault(a => a.UpstreamPathTemplate == upUrl && a.DownstreamPathTemplate == downUrl);
            if (route == null)
            {
                throw new KeyNotFoundException("路由映射不存在");
            }
            return new HttpResponseResult<FileRoute> { Result = route };
        }

        private async Task<OcelotAdminToken> GetTokenAsync()
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
