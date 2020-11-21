using System;
using System.Collections.Generic;
using HealthChecks.UI.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CodePlus.Blazor.HealthChecks
{
    public static class HealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddRedisCheck(this IHealthChecksBuilder builder, string name, string redisConnection, HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default)
        {
            throw new NotImplementedException();
        }
    }
}
