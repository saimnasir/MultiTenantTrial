using MultiTenantApp.Consts;

namespace MultiTenantApp.Services
{
    public class TenantService : ITenantService
    { 
        public string? Tenant { get; private set; }
        public void SetTenant(string? tenant)
        {
            Tenant = tenant;
        }
    }

}
