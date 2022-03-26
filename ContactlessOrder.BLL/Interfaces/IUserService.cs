using ContactlessOrder.Common.Dto.Auth;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser(string email);
    }
}
