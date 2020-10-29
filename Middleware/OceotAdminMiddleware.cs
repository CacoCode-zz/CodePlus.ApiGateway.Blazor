
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Middleware
{
    public class OceotAdminMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<OceotAdminMiddleware> _logger;

        public OceotAdminMiddleware(RequestDelegate next, ILogger<OceotAdminMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await this._next.Invoke(context);
        }
    }
}
