using AutoMapper;
using MultiTenantApp.Dto;
using MultiTenantApp.Mapper;
using MultiTenantApp.Models;
using MultiTenantApp.Repositories;
using MultiTenantApp.Services;
using Patika.Framework.Application.Services;
using Patika.Framework.Shared.DTO;
using System.Xml.Linq;

namespace MultiTenantApp.AppServices
{
    public class PassAppService : ApplicationService, IPassAppService
    {
        private IPassRepository PassRepository { get; }
        private ITenantService TenantService { get; }
        private IMapper Mapper { get; }

        public PassAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            PassRepository = GetService<IPassRepository>();
            TenantService = GetService<ITenantService>();
            Mapper = GetService<IMapper>();
        }

        public async Task<List<PassDto>> GetListAsync(DTO input)
        {
            List<Pass> passes = GetPass();
            List<Bank> banks = GetBanks();
            var passesDTO = Mapper.Map<List<PassDto>>(passes);
            var banksDTO = Mapper.Map<List<BankDto>>(banks);
            await Task.CompletedTask;
            return passesDTO;
        }

        private List<Bank> GetBanks()
        {
            return new List<Bank>
            {
                new Bank
                {
                    Kind = "Password",
                    Name = "Deneme", 
                    IsDeleted  =false,
                    Id = Guid.NewGuid(),
                    CreatedAt= DateTime.UtcNow,
                }
            };
        }

        private static List<Pass> GetPass()
        {
            return new List<Pass>
            {
                new Pass
                {
                    Kind = "Password",
                    Name = "Deneme",
                    Tenant  = "Tenant",
                    IsDeleted  =false,
                    Id = Guid.NewGuid(),
                    CreatedAt= DateTime.UtcNow,
                }
            };
        }
    }
}
