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

            CreateMap<CreateCateringDto, Catering>();
            CreateMap<CateringDto, CateringDto>();
        }
    }
}
