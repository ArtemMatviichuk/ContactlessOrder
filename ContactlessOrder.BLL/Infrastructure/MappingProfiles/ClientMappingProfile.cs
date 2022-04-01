using AutoMapper;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Entities.Companies;
using System;
using System.Linq;

namespace ContactlessOrder.BLL.Infrastructure.MappingProfiles
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Catering, ClientCateringDto>()
                .ForMember(e => e.Label, opt => opt.MapFrom(e => e.Company.Name))
                .ForMember(e => e.OpenTime, opt => opt.MapFrom(e => MapToTimeDto(e.OpenTime)))
                .ForMember(e => e.CloseTime, opt => opt.MapFrom(e => MapToTimeDto(e.CloseTime)));
        }

        private static TimeDto MapToTimeDto(TimeSpan? data)
        {
            if (!data.HasValue)
            {
                return null;
            }

            return new TimeDto() { Hour = data.Value.Hours, Minute = data.Value.Minutes };
        }
    }
}
