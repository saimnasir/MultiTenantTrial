using MultiTenantApp.Consts;

namespace MultiTenantApp.Services
{
    public class TenantService : ITenantService
    {
        public List<string> AllTenants { get; private set; }
        public string? Tenant { get; private set; }
        public void SetTenant(string? tenant)
        {
            Tenant = tenant;
        }
    }

}
