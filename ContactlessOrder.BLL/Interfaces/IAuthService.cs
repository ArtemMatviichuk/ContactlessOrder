using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Auth;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<string>> Register(UserRegisterRequestDto dto);
        Task<ResponseDto<string>> RegisterCompany(CompanyRegisterRequestDto dto);
        Task<ResponseDto<string>> GoogleLogin(GoogleRegisterRequestDto dto);
        Task<ResponseDto<string>> Authenticate(UserLoginRequestDto dto);
        Task<string> ValidateEmail(string email, int? id = null);
        Task<string> ValidatePhoneNumber(string phoneNumber, int? id = null);
        Task<string> ValidateCompanyName(string name, int? id = null);
        Task<string> ConfirmEmail(int userId);
    }
}
