using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CodePlus.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        // Set properties and call methods on options
                    })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var env = hostingContext.HostingEnvironment;
                            config.AddJsonFile("appsettings.json", true, true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                                .AddJsonFile("ocelot.json", true, true)
                                .AddJsonFile($"ocelot.{env.EnvironmentName}.json", true, true)
                                .AddEnvironmentVariables();
                        })
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name + ".Blazor")
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
