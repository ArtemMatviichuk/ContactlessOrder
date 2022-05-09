using ContactlessOrder.Common.Constants;
using Microsoft.AspNetCore.SignalR;

namespace ContactlessOrder.Api.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var id = connection.User?.FindFirst(TokenProperties.Id)?.Value;
            var role = connection.User?.FindFirst(TokenProperties.Role)?.Value;
            
            return $"{role}.{id}";
        }
    }
}
