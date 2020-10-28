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

        Task<FileConfiguration> SetConfig(string routeConfig);
    }
}
