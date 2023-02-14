using AutoMapper;
using MultiTenantApp.Dto;
using MultiTenantApp.Models;
using Patika.Framework.Application.Contracts.Mapper;
using Patika.Framework.Shared.DTO.Identity;
using Patika.Framework.Shared.Entities;

namespace MultiTenantApp.Mapper
{
   
    public class FirstProfile :  Profile
    {
        public FirstProfile()
        {
            CreateMap<Pass, PassDto>()
             .ForMember(d => d.CreatedAt, opt => opt.MapFrom<TimeZoneMemberValueResolver, DateTime>(s => s.CreatedAt));

        }
    }
}
