using ContactlessOrder.DAL.Entities.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface IAdminRepository : IRepositoryBase
    {
        Task<IEnumerable<Company>> GetCompanies(bool approved);
    }
}
