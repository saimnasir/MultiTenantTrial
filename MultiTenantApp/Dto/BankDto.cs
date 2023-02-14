using MultiTenantApp.Consts;
using Patika.Framework.Shared.DTO;

namespace MultiTenantApp.Dto
{
    public class BankDto : DTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Kind { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; } 
    }
}
