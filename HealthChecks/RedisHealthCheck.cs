using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CodePlus.Blazor.HealthChecks
{
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly string _redisConnection;

        public RedisHealthCheck(string redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
