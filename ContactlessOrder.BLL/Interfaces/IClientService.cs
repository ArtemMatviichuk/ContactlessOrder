using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.Common.Dto.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientCateringDto>> GetCaterings(GetCateringsDto dto);
        Task<IEnumerable<ClientMenuPositionDto>> GetCateringMenu(int cateringId);
        Task<IEnumerable<CartOptionDto>> GetCartData(IEnumerable<int> itemIds);
        Task<IEnumerable<AttachmentDto>> GetMenuPictures(int id);
        Task<int> CreateOrder(int userId, CreateOrderDto dto);
        Task OrderPaid(IdNameDto dto);
        Task<int> GetOrderTotalPrice(int id, int userId);
        Task<IEnumerable<OrderDto>> GetOrders(int userId);
    }
}
