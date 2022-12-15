using MultiTenantApp.Models;
using Patika.Framework.Domain.Interfaces.Repository;

namespace MultiTenantApp.Repositories
{
    public interface IPassRepository : IGenericRepository<Pass, Guid>
    {
    }
}
