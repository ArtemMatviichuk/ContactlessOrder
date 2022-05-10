using ContactlessOrder.DAL.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface ISupportRepository : IRepositoryBase
    {
        Task<IEnumerable<Complain>> GetComplains(int statusValue);
        Task<Complain> GetComplain(int id);
        Task<User> GetUser(int id);
    }
}
