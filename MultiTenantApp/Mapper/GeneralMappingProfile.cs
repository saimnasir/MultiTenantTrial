using MultiTenantApp.Dto;
using MultiTenantApp.Models;
using Patika.Framework.Application.Contracts.Mapper;
using Patika.Framework.Shared.DTO.Identity;
using Patika.Framework.Shared.Entities;

namespace MultiTenantApp.Mapper
{
    public class GeneralMappingProfile : MappingProfile
    {
        protected override void Configure()
        {

            CreateMapTwoSide<Pass, PassDto>();
        }
    }
}
