using Microsoft.EntityFrameworkCore;
using MultiTenantApp.Database;
using MultiTenantApp.Models; 
using Patika.Framework.Domain.Services;

namespace MultiTenantApp.Repositories
{
    public class PassRepository : GenericRepository<Pass, IpassDbContext, Guid>, IPassRepository
    {
        IServiceProvider ServiceProvider { get; }
        public PassRepository(DbContextOptions<IpassDbContext> options, IServiceProvider serviceProvider) : base(options)
        {
            ServiceProvider = serviceProvider;
        }

        protected override IpassDbContext GetContext() => new(DbOptions, ServiceProvider);

        protected override IQueryable<Pass> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<Pass>(); 
    }
}