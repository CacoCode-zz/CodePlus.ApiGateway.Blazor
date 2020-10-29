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
        Task<FileConfiguration> GetConfig();

        Task<HttpResponseResult<FileConfiguration>> SetConfig(string routeConfig);

        Task<HttpResponseResult<FileConfiguration>> SetConfig(FileRoute route);
    }
}
