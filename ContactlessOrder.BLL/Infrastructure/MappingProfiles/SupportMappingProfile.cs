using AutoMapper;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Users;

namespace ContactlessOrder.BLL.Infrastructure.MappingProfiles
{
    public class SupportMappingProfile : Profile
    {
        public SupportMappingProfile()
        {
            CreateMap<Complain, ComplainDto>()
                .ForMember(e => e.UserName, opt => opt.MapFrom(e => $"{e.User.FirstName} {e.User.LastName}"))
                .ForMember(e => e.OrderNumber, opt => opt.MapFrom(e => $"#{e.OrderId:d16}"))
                .ForMember(e => e.CompanyId, opt => opt.MapFrom(e => e.Catering.CompanyId))
                .ForMember(e => e.CompanyName, opt => opt.MapFrom(e => e.Catering.Company.Name))
                .ForMember(e => e.CompanyEmail, opt => opt.MapFrom(e => e.Catering.Company.User.Email))
                .ForMember(e => e.CompanyPhoneNumber, opt => opt.MapFrom(e => e.Catering.Company.User.PhoneNumber));
        }
    }
}
