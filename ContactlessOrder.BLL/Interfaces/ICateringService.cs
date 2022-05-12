using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface ICateringService
    {
        Task<IEnumerable<CateringMenuOptionDto>> GetMenu(int userId);
        Task<string> UpdateMenuOption(int id, UpdateCateringMenuOptionDto dto);
        Task<IEnumerable<OrderDto>> GetOrders(int userId);
        Task<IEnumerable<OrderDto>> GetEndedOrders(int userId);

        Task<IEnumerable<CateringModificationDto>> GetModifications(int userId);
        Task UpdateModification(int id, int userId, UpdateCateringMenuOptionDto dto);
        Task<IEnumerable<IdNameValueDto>> GetOrderStatuses();
        Task UpdateOrderStatus(int orderId, int statusId);
    }
}
