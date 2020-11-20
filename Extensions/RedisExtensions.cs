using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodePlus.Blazor.Extensions
{
    public static class RedisExtensions
    {
        public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            RedisHelper.Initialization(new CSRedis.CSRedisClient(configuration["Redis"]));
        }
    }
}
