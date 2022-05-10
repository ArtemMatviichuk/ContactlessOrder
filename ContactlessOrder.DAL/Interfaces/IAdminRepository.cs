using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface IAdminRepository : IRepositoryBase
    {
        Task<IEnumerable<Company>> GetCompanies(bool approved);
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetSupport();
        Task<IEnumerable<User>> GetAdministrators();
    }
}
