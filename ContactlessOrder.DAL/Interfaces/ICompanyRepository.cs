using ContactlessOrder.DAL.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface ICompanyRepository : IRepositoryBase
    {
        Task<Company> GetCompany(int userId);
    }
}
