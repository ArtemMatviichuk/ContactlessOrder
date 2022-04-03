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

            CreateMap<CateringMenuOption, CartOptionDto>()
                    .ForMember(e => e.Id, opt => opt.MapFrom(option => option.MenuOption.Id))
                    .ForMember(e => e.Price, opt => opt.MapFrom(option => option.Price ?? option.MenuOption.Price))
                    .ForMember(e => e.Name, opt => opt.MapFrom(option => $"{option.MenuOption.MenuItem.Name} ({ option.MenuOption.Name})"))
                    .ForMember(e => e.CompanyName, opt => opt.MapFrom(option => option.Catering.Company.Name))
                    .ForMember(e => e.FirstPictureId, opt => opt.MapFrom(option => option.MenuOption.MenuItem.Pictures.FirstOrDefault().Id));
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
