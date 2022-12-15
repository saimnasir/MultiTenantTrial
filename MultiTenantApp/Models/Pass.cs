using MultiTenantApp.Consts;
using Patika.Framework.Shared.Entities;
using Patika.Framework.Shared.Interfaces;

namespace MultiTenantApp.Models
{
    public class Pass : GenericEntity<Guid>, ILogicalDelete
    {
        public string Name { get; set; } = string.Empty;
        public string Kind { get; set; } = string.Empty;
        public string Tenant { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
