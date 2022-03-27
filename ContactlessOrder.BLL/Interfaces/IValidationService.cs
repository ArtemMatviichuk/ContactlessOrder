using ContactlessOrder.Common.Dto.Auth;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface IValidationService
    {
        Task<string> ValidateUser(UserRegisterRequestDto dto, int? id = null);
        Task<string> ValidateCompany(CompanyRegisterRequestDto dto, int? userId = null);
        Task<string> ValidateEmail(string email, int? id = null);
        Task<string> ValidatePhoneNumber(string phoneNumber, int? id = null);
        Task<string> ValidateCompanyName(string name, int? userId = null);
    }
}
