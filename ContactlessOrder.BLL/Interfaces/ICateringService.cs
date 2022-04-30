using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface ICateringService
    {
        Task<IEnumerable<CateringMenuOptionDto>> GetMenu(int cateringId);
        Task<string> UpdateMenuOption(int id, UpdateCateringMenuOptionDto dto);
        Task<IEnumerable<OrderDto>> GetOrders(int cateringId);
        Task<IEnumerable<OrderDto>> GetEndedOrders(int cateringId);

        Task<IEnumerable<CateringModificationDto>> GetModifications(int cateringId);
        Task UpdateModification(int id, int cateringId, UpdateCateringMenuOptionDto dto);
        Task<IEnumerable<IdNameValueDto>> GetOrderStatuses();
        Task UpdateOrderStatus(int orderId, int statusId);
    }
}
