using ContactlessOrder.Common.Dto.Orders;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.HubConnections.HubClients
{
    public interface ISupportHubClient
    {
        Task ComplainAdded(ComplainDto dto);
        Task ComplainUpdated(ComplainDto dto);
    }
}
