using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CodePlus.Blazor.Extensions
{
    public static class HealthCheckExtensions
    {

        public static IServiceCollection AddHealthChecks(
             this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecksService = services
                .AddHealthChecks();

            //添加对Redis的监控检查
            if (Convert.ToBoolean(configuration["HealthChecksUI:HealthCheckItem:Redis:IsEnable"]))
                healthChecksService.AddRedis(
                    configuration["HealthChecksUI:HealthCheckItem:Redis:ConnectionString"],
                    tags: new string[] { "redis" }
                );

            //添加对Pings的监控检查
            if (Convert.ToBoolean(configuration["HealthChecksUI:HealthCheckItem:Ping:IsEnable"]))
                healthChecksService.AddPingHealthCheck(setup =>
                {
                    setup.AddHost(configuration["HealthChecksUI:HealthCheckItem:Ping:Host"], Convert.ToInt32(configuration["HealthChecksUI:HealthCheckItem:Ping:TimeOut"]));
                }, tags: new string[] { "ping" });

            return services;
        }
    }
}
