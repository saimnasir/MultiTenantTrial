using MultiTenantApp.Dto;
using MultiTenantApp.Mapper;
using MultiTenantApp.Repositories;
using MultiTenantApp.Services;
using Patika.Framework.Application.Services;
using Patika.Framework.Shared.DTO;

namespace MultiTenantApp.AppServices
{
    public class PassAppService : ApplicationService, IPassAppService
    {
        private IPassRepository PassRepository { get; } 
        private ITenantService TenantService { get; } 

        public PassAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            PassRepository = GetService<IPassRepository>();
            TenantService = GetService<ITenantService>(); 
            MappingProfile = new GeneralMappingProfile();
        }

        public async Task<List<PassDto>> GetListAsync(DTO input)
        {
            if (string.IsNullOrEmpty(TenantService.Tenant?.Trim())){
                throw new Exception("Tenant Invalid");
            }
            var all = await PassRepository.GetAllAsync(); 
            var result = Mapper.Map<List<PassDto>>(all);
            return result;
        }      
    }
}
