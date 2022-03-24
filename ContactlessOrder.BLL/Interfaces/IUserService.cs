using ContactlessOrder.Common.Dto.Users;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser(string username);
    }
}
