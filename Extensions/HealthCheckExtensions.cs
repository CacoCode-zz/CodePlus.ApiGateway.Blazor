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

            healthChecksService.AddRedis(
                configuration["Redis"],
                tags: new string[] { "redis" }
            );

            //if (Convert.ToBoolean(configuration["HealthChecks-UI:IsEnable"]))
            //{
                //var healthChecksService = services
                //    .AddHealthChecks();

                ////添加对sql server的监控检查
                //if (Convert.ToBoolean(configuration["HealthChecks-UI:SqlServer:IsEnable"]))
                //    healthChecksService.AddSqlServer(
                //        configuration["ConnectionStrings:Default"],
                //        configuration["HealthChecks-UI:SqlServer:HealthQuery"],
                //        configuration["HealthChecks-UI:SqlServer:Name"],
                //        HealthStatus.Degraded,
                //        new string[] { "db", "sql", "sqlserver" }
                //        );

                ////添加对Redis的监控检查
                //if (Convert.ToBoolean(configuration["HealthChecks-UI:Redis:IsEnable"]))
                //    healthChecksService.AddRedis(
                //        configuration["Abp:RedisCache:ConnectionString"],
                //        tags: new string[] { "redis" }
                //    );

                ////添加对Hangfire的监控检查
                //if (Convert.ToBoolean(configuration["HealthChecks-UI:Hangfire:IsEnable"]))
                //    healthChecksService.AddHangfire(options =>
                //    {
                //        options.MaximumJobsFailed = Convert.ToInt32(configuration["HealthChecks-UI:Hangfire:MaximumJobsFailed"]);
                //        options.MinimumAvailableServers = Convert.ToInt32(configuration["HealthChecks-UI:Hangfire:MinimumAvailableServers"]);
                //    }, tags: new string[] { "hangfire" });

                ////添加对SignalR的监控检查
                //if (Convert.ToBoolean(configuration["HealthChecks-UI:SignalR:IsEnable"]))
                //    healthChecksService.AddSignalRHub(configuration["HealthChecks-UI:SignalR:Url"], tags: new string[] { "signalr" });

                ////添加对Pings的监控检查
                //if (Convert.ToBoolean(configuration["HealthChecks-UI:Pings:IsEnable"]))
                //    healthChecksService.AddPingHealthCheck(setup =>
                //    {
                //        setup.AddHost(configuration["HealthChecks-UI:Pings:Host"], Convert.ToInt32(configuration["HealthChecks-UI:Pings:TimeOut"]));
                //    }, tags: new string[] { "ping" });

            //}
            return services;
        }
    }
}
