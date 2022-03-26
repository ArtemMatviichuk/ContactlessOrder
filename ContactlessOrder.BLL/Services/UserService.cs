using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Users;
using ContactlessOrder.DAL.Entities.User;
using ContactlessOrder.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUser(string email)
        {
            var user = await _userRepository.Get<User>(e => e.Email == email);
            
            if (user == null)
            {
                return null;
            }

            var dto = _mapper.Map<UserDto>(user);

            return dto;
        }
    }
}
