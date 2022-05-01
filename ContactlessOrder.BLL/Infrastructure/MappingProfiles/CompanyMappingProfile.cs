using AutoMapper;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Entities.Companies;
using System;
using System.Linq;

namespace ContactlessOrder.BLL.Infrastructure.MappingProfiles
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(e => e.Email, opt => opt.MapFrom(e => e.User.Email))
                .ForMember(e => e.PhoneNumber, opt => opt.MapFrom(e => e.User.PhoneNumber))
                .ForMember(e => e.RegisteredDate, opt => opt.MapFrom(e => e.User.RegistrationDate))
                .ForMember(e => e.ModifiedDate, opt => opt.MapFrom(e => e.User.ModifiedDate))
                .ForMember(e => e.ApprovedByName, opt => opt.MapFrom(e => $"{e.ApprovedBy.FirstName} {e.ApprovedBy.LastName}"));

            CreateMap<Coordinate, CoordinateDto>().ReverseMap();
            CreateMap<PaymentData, PaymentDataDto>().ReverseMap();

            CreateMap<CreateCateringDto, Catering>()
                .ForMember(e => e.OpenTime, opt => opt.MapFrom(e => MapToTimeSpan(e.OpenTime)))
                .ForMember(e => e.CloseTime, opt => opt.MapFrom(e => MapToTimeSpan(e.CloseTime)));
            CreateMap<Catering, CateringDto>()
                .ForMember(e => e.OpenTime, opt => opt.MapFrom(e => MapToTimeDto(e.OpenTime)))
                .ForMember(e => e.CloseTime, opt => opt.MapFrom(e => MapToTimeDto(e.CloseTime)))
                .ForMember(e => e.MenuIds, opt => opt.MapFrom(e => e.MenuOptions.Select(e => e.MenuOptionId)));


            CreateMap<MenuItemPicture, AttachmentDto>();
            CreateMap<MenuItemOption, IdNamePriceDto>().ReverseMap();
            CreateMap<Modification, IdNamePriceDto>();
            CreateMap<NamePriceDto, Modification>();

            CreateMap<CreateMenuItemDto, MenuItem>()
                .ForMember(e => e.Pictures, opt => opt.Ignore());

            CreateMap<UpdateMenuItemDto, MenuItem>()
                .ForMember(e => e.Pictures, opt => opt.Ignore());

            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(e => e.FirstPictureId, opt => opt.MapFrom(e => e.Pictures.FirstOrDefault().Id))
                .ForMember(e => e.Modifications, opt => opt.MapFrom(e => e.MenuItemModifications.Select(e => e.ModificationId)));
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
