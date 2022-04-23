using AutoMapper;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Orders;
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
                    .ForMember(e => e.CateringOptionId, opt => opt.MapFrom(option => option.Id))
                    .ForMember(e => e.Price, opt => opt.MapFrom(option => option.Price ?? option.MenuOption.Price))
                    .ForMember(e => e.Name, opt => opt.MapFrom(option => $"{option.MenuOption.MenuItem.Name} ({ option.MenuOption.Name})"))
                    .ForMember(e => e.CompanyName, opt => opt.MapFrom(option => option.Catering.Company.Name))
                    .ForMember(e => e.FirstPictureId, opt => opt.MapFrom(option => option.MenuOption.MenuItem.Pictures.FirstOrDefault().Id));

            CreateMap<Order, OrderDto>()
                .ForMember(e => e.Number, opt => opt.MapFrom(e => $"#{e.Id:d16}"))
                .ForMember(e => e.Positions, opt => opt.MapFrom(e => e.Positions.Select(p => MapPosition(p))));
        }

        private static TimeDto MapToTimeDto(TimeSpan? data)
        {
            if (!data.HasValue)
            {
                return null;
            }

            return new TimeDto() { Hour = data.Value.Hours, Minute = data.Value.Minutes };
        }

        private static OrderPositionDto MapPosition(OrderPosition position)
        {
            return new OrderPositionDto()
            {
                OptionId = position.Id,
                OptionName = position.OptionId != null
                    ? $"{position.Option.MenuOption.MenuItem.Name} ({ position.Option.MenuOption.Name})"
                    : position.OptionName,
                Quantity = position.Quantity,
                PictureId = position.Option?.MenuOption.MenuItem.Pictures.FirstOrDefault()?.Id,
            };
        }
    }
}
