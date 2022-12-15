using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantApp.AppServices;
using MultiTenantApp.Dto;
using Patika.Framework.Shared.Controllers;
using Patika.Framework.Shared.DTO;

namespace MultiTenantApp.Controllers
{
    [Route("api/[controller]/")]
    public class PassesController : GenericApiController
    {
        IPassAppService PassAppService { get; }
        public PassesController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            PassAppService = GetService<IPassAppService>();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<FinalResponseDTO<List<PassDto>>>> GetListAsync(string? tenant)
        {
            var input = new DTO
            {  
            };
            return await WithLoggingFinalResponse(input, async () =>
            {
                return await PassAppService.GetListAsync(input);
            });
        }
    }
}
