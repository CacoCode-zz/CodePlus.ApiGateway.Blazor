using CodePlus.Blazor.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseOceotAdminMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OceotAdminMiddleware>();
        }
    }
}
