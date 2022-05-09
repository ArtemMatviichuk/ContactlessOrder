using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<CompanyDto>> GetCompanies(bool approved);
        Task ApproveCompany(int id, int userId);
        Task RejectCompany(int id);
        Task<PaymentDataDto> GetCompanyPaymentData(int id);
        Task<IEnumerable<CateringDto>> GetCaterings(int id);
    }
}
