using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CodePlus.Blazor.Data;
using Serilog;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Administration;
using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CodePlus.Blazor
{
    public class Startup
    {
        private const string DefaultCorsPolicy = "AllowAny";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient(AppConsts.DefaultHttpClient, cc =>
            {
                cc.BaseAddress = new Uri(Configuration["DefaultHttp"]);
            });
            services.AddCors(options =>
                options.AddPolicy(DefaultCorsPolicy, p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
            );
            RedisHelper.Initialization(new CSRedis.CSRedisClient(Configuration["Redis:Configuration"]));
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAntDesign();
            services.AddOcelot(Configuration)
               .AddConsul()
               .AddAdministration("/administration", "secret");
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ILogger<Startup> logger)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(DefaultCorsPolicy);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("{*path:regex(^(?![administration|api]).*$)}", "/_Host");
            });
            app.UseOcelot().Wait();

            logger.LogInformation($"Environment:{env.EnvironmentName}{Environment.NewLine}" +
                       $"Ocelot:{Configuration["GlobalConfiguration:BaseUrl"]}{Environment.NewLine}");
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                    .Where(x => x.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
        }
    }
}
