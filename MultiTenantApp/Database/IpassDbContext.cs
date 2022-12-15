using Microsoft.EntityFrameworkCore;
using MultiTenantApp.Models;
using MultiTenantApp.Services;
using Patika.Framework.Domain.Services;
using Patika.Framework.Shared.Exceptions;
using Patika.Framework.Shared.Extensions;

namespace MultiTenantApp.Database
{
    public class IpassDbContext : DbContextWithUnitOfWork<IpassDbContext>
    {
        private ITenantService TenantService { get; }
        public IpassDbContext(DbContextOptions<IpassDbContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            TenantService = serviceProvider.GetService<ITenantService>() ?? throw new ServiceNotInjectedException($"{typeof(ITenantService).FullName}");
        }
 
        public DbSet<Pass> Passes { get; set; } = default!; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Pass>()
                .HasQueryFilter(a => a.Tenant == TenantService.Tenant)
                //.HasData(
                //    new() { Id = Guid.NewGuid().NewSequentalGuid(), Kind = "Dog", Name = "Samson", Tenant = "Khalid" },
                //    new() { Id = Guid.NewGuid().NewSequentalGuid(), Kind = "Dog", Name = "Guinness", Tenant = "Khalid" },
                //    new() { Id = Guid.NewGuid().NewSequentalGuid(), Kind = "Cat", Name = "Grumpy Cat", Tenant = "Internet" },
                //    new() { Id = Guid.NewGuid().NewSequentalGuid(), Kind = "Cat", Name = "Mr. Bigglesworth", Tenant = "Internet" }
                //)
                ;
        }
    }

}