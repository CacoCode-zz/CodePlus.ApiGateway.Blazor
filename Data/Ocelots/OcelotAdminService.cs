using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ocelot.Configuration.File;
using CodePlus.ApiGateway.Data.Https;
using CodePlus.ApiGateway.Data.Logins;
using CodePlus.ApiGateway.Data.Ocelots.Dto;

namespace CodePlus.ApiGateway.Data.Ocelots
{
    public class OcelotAdminService : IOcelotAdminService
    {
        private readonly IHttpUtilityService _httpUtilityService;
        private readonly IConfiguration _configuration;


        public OcelotAdminService(IHttpUtilityService httpUtilityService, IConfiguration configuration)
        {
            _httpUtilityService = httpUtilityService;
            _configuration = configuration;
        }

        public async Task<FileConfiguration> GetConfigAsync(string token)
        {
            var data = await GetConfiguration(token);
            return data;
        }

        public async Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(string routeConfig, string token)
        {
            var routeObject = JsonConvert.DeserializeObject<FileRoute>(routeConfig);
            var oldConfig = await GetConfiguration(token);
            oldConfig.Routes.Add(routeObject);
            var result = await AddOrUpdateConfiguration(oldConfig, token);
            return result;
        }

        public async Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(FileRoute route, string token)
        {
            var oldConfig = await GetConfiguration(token);
            oldConfig.Routes.Add(route);
            var result = await AddOrUpdateConfiguration(oldConfig, token);
            if (!string.IsNullOrWhiteSpace(result.HttpResponseMessages))
            {
                oldConfig.Routes.Remove(oldConfig.Routes.Last());
                await AddOrUpdateConfiguration(oldConfig, token);
            }
            return result;
        }

        public async Task<HttpResponseResult<FileConfiguration>> UpdateConfigAsync(FileRoute fileRoute, string token)
        {
            var data = await GetConfiguration(token);
            for (int i = 0; i < data.Routes.Count; i++)
            {
                var item = data.Routes[i];
                if (item.UpstreamPathTemplate == fileRoute.UpstreamPathTemplate &&
                                    item.DownstreamPathTemplate == fileRoute.DownstreamPathTemplate)
                {
                    data.Routes[i] = fileRoute;
                }
            }
            var result = await AddOrUpdateConfiguration(data, token);
            return result;
        }

        public async Task<HttpResponseResult<FileConfiguration>> DeleteConfigAsync(string upUrl, string downUrl, string token)
        {
            var data = await GetConfiguration(token);
            var route = data.Routes.FirstOrDefault(a => a.UpstreamPathTemplate == upUrl && a.DownstreamPathTemplate == downUrl);
            if (route == null)
            {
                throw new Exception("路由映射不存在");
            }
            data.Routes.Remove(route);
            var result = await AddOrUpdateConfiguration(data, token);
            return result;
        }

        public async Task<HttpResponseResult<FileRoute>> GetConfigByAsync(string upUrl, string downUrl, string token)
        {
            var data = await GetConfiguration(token);
            var route = data.Routes.FirstOrDefault(a => a.UpstreamPathTemplate == upUrl && a.DownstreamPathTemplate == downUrl);
            if (route == null)
            {
                throw new Exception("路由映射不存在");
            }
            return new HttpResponseResult<FileRoute> { Result = route };
        }

        public async Task<OcelotAdminToken> GetTokenAsync(LoginInputDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Password))
            {
                throw new Exception("密钥不能为空！");
            }
            if (input.Password != _configuration["OcelotSecret"])
            {
                throw new Exception("密钥错误！");
            }
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", "admin");
            dic.Add("client_secret", input.Password);
            dic.Add("scope", "admin");
            dic.Add("grant_type", "client_credentials");
            var ocelotToken = await _httpUtilityService.PostFormDataAsync<OcelotAdminToken>("/administration/connect/token", dic);
            return ocelotToken;
        }

        private async Task<FileConfiguration> GetConfiguration(string accessToken)
        {
            var result = await _httpUtilityService.GetAsync<FileConfiguration>("/administration/configuration", accessToken);
            return result;
        }

        private async Task<HttpResponseResult<FileConfiguration>> AddOrUpdateConfiguration(FileConfiguration data, string accessToken)
        {
            var result = await _httpUtilityService.PostAsync<FileConfiguration>("/administration/configuration", data, accessToken);
            return result;
        }
    }
}
