using Susalem.Core.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Susalem.Infrastructure.Middleware
{
    public class TenantInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantInfo = context.RequestServices.GetRequiredService<TenantInfo>();
            var tenantName = context.Request.Headers["Tenant"];
            
            if (!string.IsNullOrEmpty(tenantName))
            {
                tenantInfo.Year = int.Parse(tenantName);
            }
            await _next(context);
        }
    }
}
