using ContactlessOrder.Common.Constants;
using Microsoft.AspNetCore.SignalR;

namespace ContactlessOrder.Api.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var prefix = connection.User?.FindFirst(TokenProperties.CateringId)?.Value;
            return prefix != null
                ? NotificationConstants.CateringPrefix
                : NotificationConstants.UserPrefix
                + connection.User?.FindFirst("Id")?.Value;
        }
    }
}
