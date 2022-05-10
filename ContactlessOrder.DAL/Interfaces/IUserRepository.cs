using ContactlessOrder.DAL.Entities.Users;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase
    {
        Task<User> GetUser(int id);
        Task<User> GetUser(string email);
    }
}
