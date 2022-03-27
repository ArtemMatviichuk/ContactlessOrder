using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompany(int userId);
        Task<FileDto> GetCompanyLogo(int userId);
        Task<string> UpdateCompanyData(int userId, UpdateCompanyDataDto dto);
    }
}
