using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Patika.Framework.Shared.Exceptions;
using Patika.Framework.Shared.Extensions;
using Patika.Framework.Shared.Interfaces;
using Patika.Framework.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace MultiTenantApp.Mapper
{

    public class TimeZoneMemberValueResolver : IMemberValueResolver<object, object, DateTime, DateTime>
    {
        private TimeZoneInfo TimeZone { get; set; } = TimeZoneInfo.Local;
        private string Language { get; set; } = "tr";
        IClientInformationService? ClientInformationService { get; set; }
        //public TimeZoneMemberValueResolver()
        //{
        //}

        public TimeZoneMemberValueResolver(IClientInformationService clientInformationService)
        {
            TimeZone = clientInformationService.TimeZoneInfo;
            Language = clientInformationService.Language;
            ClientInformationService = clientInformationService;
        }

        public DateTime Resolve(object source, object destination, DateTime sourceMember, DateTime destMember, ResolutionContext context)
        {
            Console.WriteLine($"Lang: {Language}");

            var date = new DateTime(sourceMember.Ticks, DateTimeKind.Utc);
            var result = date.ConvertTimeFromUtc(TimeZone); 
            return result;
        }
    }
}
