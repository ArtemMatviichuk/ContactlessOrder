using AutoMapper;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Entities.Companies;
using System;

namespace ContactlessOrder.BLL.Infrastructure.MappingProfiles
{
    public class CompanyMappingProfile : Profile
    {

        public DateTime RegisteredDate { get; set; }
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(e => e.Email, opt => opt.MapFrom(e => e.User.Email))
                .ForMember(e => e.PhoneNumber, opt => opt.MapFrom(e => e.User.PhoneNumber))
                .ForMember(e => e.RegisteredDate, opt => opt.MapFrom(e => e.User.RegistrationDate))
                .ForMember(e => e.ModifiedDate, opt => opt.MapFrom(e => e.User.ModifiedDate));

            CreateMap<CreateCateringDto, Catering>()
                .ForMember(e => e.OpenTime, opt => opt.MapFrom(e => MapToTimeSpan(e.OpenTime)))
                .ForMember(e => e.CloseTime, opt => opt.MapFrom(e => MapToTimeSpan(e.CloseTime)));
            CreateMap<Catering, CateringDto>()
                .ForMember(e => e.OpenTime, opt => opt.MapFrom(e => MapToTimeDto(e.OpenTime)))
                .ForMember(e => e.CloseTime, opt => opt.MapFrom(e => MapToTimeDto(e.CloseTime)));
        }

        private static TimeSpan? MapToTimeSpan(TimeDto dto)
        {
            return dto == null ? null : new TimeSpan(dto.Hour, dto.Minute, 0);
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
