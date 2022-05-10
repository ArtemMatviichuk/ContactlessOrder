using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface INotificationService
    {
        Task NotifyOrderPaid(int id, int totalPrice);
        Task NotifyOrderUpdated(int id, int totalPrice);
        Task NotifyOrderReady(int id); 
        Task NotifyOrderRejected(int id, int totalPrice);
        Task NotifyOrderCompleted(int id, int totalPrice);

        Task NotifyComplainAdded(int id);
        Task NotifyComplainUpdated(int id);

        Task NotifyCompanyAdded(int userId);
        Task NotifyCompanyUpdated(int userId);

        Task NotifyUserRegistered(int id);
        Task NotifyUserUpdated(int id);
        Task NotifyUserDeleted(int id);
    }
}