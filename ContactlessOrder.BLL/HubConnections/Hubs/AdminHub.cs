using ContactlessOrder.BLL.HubConnections.HubClients;
using Microsoft.AspNetCore.SignalR;

namespace ContactlessOrder.BLL.HubConnections.Hubs
{
    public class AdminHub : Hub<IAdminHubClient>
    {
    }
}
