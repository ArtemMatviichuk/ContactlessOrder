using AutoMapper;
using ContactlessOrder.Common.Dto.Users;
using ContactlessOrder.DAL.Entities.User;

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
