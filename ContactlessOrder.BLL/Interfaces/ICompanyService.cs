using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Companies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompany(int userId);
        Task<FileDto> GetCompanyLogo(int userId);
        Task<string> UpdateCompanyData(int userId, UpdateCompanyDataDto dto);

        Task<IEnumerable<CateringDto>> GetCaterings(int userId);
        Task<UserLoginRequestDto> CreateCatering(int userId, CreateCateringDto dto);
        Task UpdateCatering(int id, CreateCateringDto dto);
        Task DeleteCatering(int id);
        Task<string> RegenerateCateringPassword(int id);
    }
}
