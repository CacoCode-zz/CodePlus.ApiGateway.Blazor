using CodePlus.Blazor.Data.Https;
using Ocelot.Configuration.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Data.Ocelots
{
    public interface IOcelotAdminService
    {
        Task<FileConfiguration> GetConfigAsync();

        Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(string routeConfig);

        Task<HttpResponseResult<FileConfiguration>> SetConfigAsync(FileRoute route);

        Task<HttpResponseResult<FileConfiguration>> UpdateConfigAsync(FileRoute fileRoute);

        Task<HttpResponseResult<FileRoute>> GetConfigByAsync(string upUrl, string downUrl);

        Task<HttpResponseResult<FileConfiguration>> DeleteConfigAsync(string upUrl, string downUrl);
    }
}
