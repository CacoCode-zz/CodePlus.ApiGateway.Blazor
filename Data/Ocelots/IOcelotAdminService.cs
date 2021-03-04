using System.Threading.Tasks;
using Ocelot.Configuration.File;
using CodePlus.ApiGateway.Data.Https;
using CodePlus.ApiGateway.Data.Logins;
using CodePlus.ApiGateway.Data.Ocelots.Dto;

namespace CodePlus.ApiGateway.Data.Ocelots
{
    public interface IOcelotAdminService
    {
        Task<FileConfiguration> GetConfigAsync(string token);

        Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(string routeConfig, string token);

        Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(FileRoute route, string token);

        Task<HttpResponseResult<FileConfiguration>> UpdateConfigAsync(FileRoute fileRoute, string token);

        Task<HttpResponseResult<FileRoute>> GetConfigByAsync(string upUrl, string downUrl, string token);

        Task<HttpResponseResult<FileConfiguration>> DeleteConfigAsync(string upUrl, string downUrl, string token);

        Task<OcelotAdminToken> GetTokenAsync(LoginInputDto input);
    }
}
