using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface INotificationService
    {
        Task NotifyOrderUpdated(int id);
        Task NotifyOrderReady(int id);
    }
}