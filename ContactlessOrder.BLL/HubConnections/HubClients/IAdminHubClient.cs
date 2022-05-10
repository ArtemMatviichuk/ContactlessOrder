using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.Common.Dto.Companies;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.HubConnections.HubClients
{
    public interface IAdminHubClient
    {
        Task CompanyCreated(CompanyDto dto);
        Task CompanyUpdated(CompanyDto dto);

        Task UserRegistered(UserDto dto);
        Task UserUpdated(UserDto dto);
        Task UserDeleted(int id);
    }
}
