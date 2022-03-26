using AutoMapper;
using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.DAL.Entities.Users;

namespace ContactlessOrder.BLL.Infrastructure.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterRequestDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
