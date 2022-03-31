using AutoMapper;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Entities.Companies;
using System;
using System.Linq;

namespace ContactlessOrder.BLL.Infrastructure.MappingProfiles
{
    public class CateringMappingProfile : Profile
    {
        public CateringMappingProfile()
        {
            CreateMap<CateringMenuOption, CateringMenuOptionDto>()
                .ForMember(e => e.Name, opt => opt.MapFrom(e => $"{e.MenuOption.MenuItem.Name} ({e.MenuOption.Name})"))
                .ForMember(e => e.Price, opt => opt.MapFrom(e => e.Price ?? e.MenuOption.Price))
                .ForMember(e => e.Description, opt => opt.MapFrom(e => e.MenuOption.MenuItem.Description));
        }
    }
}
