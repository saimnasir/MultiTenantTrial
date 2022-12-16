using MultiTenantApp.Consts;
using MultiTenantApp.Services;
using Patika.Framework.Identity.Shared.Attributes;
using Patika.Framework.Shared.Services;

namespace MultiTenantApp.Middlewares
{
    public class MultiTenantServiceMiddleware : CoreService, IMiddleware
    {
        private readonly ITenantService TenantService;
        public MultiTenantServiceMiddleware(IServiceProvider serviceProvider): base(serviceProvider)
        {
            TenantService = GetService<ITenantService>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            TenantService.SetTenant(string.Empty);
            if (context.Request.Query.TryGetValue("tenant", out var values))
            {
                var tenant = Tenants.Find(values.FirstOrDefault());
                TenantService.SetTenant(tenant);
            }
          
            await next(context);
        }
    }
}
