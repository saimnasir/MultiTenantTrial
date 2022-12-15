using MultiTenantApp.Dto;
using Patika.Framework.Application.Contracts.Interfaces;
using Patika.Framework.Shared.DTO;

namespace MultiTenantApp.AppServices
{
    public interface IPassAppService : IApplicationService
    {
        Task<List<PassDto>> GetListAsync(DTO input);
    }
}
