using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZemogaRon.Filters
{
    public class ApiKeyAuth : Attribute , IAsyncActionFilter
    {
        private const string ApiKeyHeader = "KeyToLogin";

        public ApiKeyAuth(string v)
        {
            V = v;
        }

        public string V { get; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next) 
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var potentialApiKey)) {

                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKeys = configuration.GetSection("MyKeys").GetChildren().Select(x => x.Value).Where(y => y == V && y == potentialApiKey).ToList();

            if (apiKeys.Count == 0) {

                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
