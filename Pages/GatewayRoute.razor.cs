using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CodePlus.Blazor.Data.Ocelots;
using Ocelot.Configuration.File;
using CodePlus.Blazor.Data.Ocelots.Dto;

namespace CodePlus.Blazor.Pages
{
    public sealed partial class GatewayRoute
    {
        private List<GatewayRouteDto> GatewayRoutes = new List<GatewayRouteDto>();

        protected override async Task OnInitializedAsync()
        {
            var ocelotConfig = await OcelotAdminService.GetConfig();
            GatewayRoutes = ocelotConfig.Routes.Select(a => new GatewayRouteDto
            {
                DownstreamHostAndPorts = string.Join(',', a.DownstreamHostAndPorts.Select(c => $"{c.Host}:{c.Port}")),
                DownstreamHttpMethod = a.DownstreamHttpMethod,
                UpstreamHttpMethod = string.Join(',', a.UpstreamHttpMethod.Select(c => c)),
                DownstreamPathTemplate = a.DownstreamPathTemplate,
                DownstreamScheme = a.DownstreamScheme,
                UpstreamPathTemplate = a.UpstreamPathTemplate
            }).ToList();
        }

        private Task DownloadAsync(IEnumerable<GatewayRouteDto>  gatewayRouteDto)
        {
            ToastService.Success();
            return Task.CompletedTask;
        }
    }
}
