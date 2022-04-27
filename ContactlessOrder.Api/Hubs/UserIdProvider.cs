using ContactlessOrder.Common.Constants;
using Microsoft.AspNetCore.SignalR;

namespace ContactlessOrder.Api.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var cateringId = connection.User?.FindFirst(TokenProperties.CateringId)?.Value;
            return cateringId != null
                ? $"{NotificationConstants.CateringPrefix}{cateringId}"
                : $"{NotificationConstants.UserPrefix}{connection.User?.FindFirst("Id")?.Value}";
        }
    }
}
