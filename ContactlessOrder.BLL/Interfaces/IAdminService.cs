using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
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

        Task<IEnumerable<UserDto>> GetUsers();
        Task<IEnumerable<UserDto>> GetSupport();
        Task<IEnumerable<UserDto>> GetAdministrators();
        Task BlockUser(int id);
        Task<string> DeleteUser(int id, int userId);
        Task<ResponseDto<string>> RegenerateUserPassword(int id);
        Task<UserLoginRequestDto> CreateUser(CreateUserDto dto);
    }
}
