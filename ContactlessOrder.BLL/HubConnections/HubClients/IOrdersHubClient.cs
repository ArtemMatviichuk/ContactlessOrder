using ContactlessOrder.Common.Dto.Orders;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.HubConnections.HubClients
{
    public interface IOrdersHubClient
    {
        Task OrderUpdated(OrderDto dto);
        Task OrderReady(string orderId);
        Task OrderPaid(OrderDto dto);
    }
}
