using ContactlessOrder.BLL.HubConnections.HubClients;
using Microsoft.AspNetCore.SignalR;

namespace ContactlessOrder.BLL.HubConnections.Hubs
{
    public class OrdersHub : Hub<IOrdersHubClient>
    {
    }
}
