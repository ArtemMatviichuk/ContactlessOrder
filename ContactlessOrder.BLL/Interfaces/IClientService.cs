﻿using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientCateringDto>> GetCaterings(GetCateringsDto dto, string search);
        Task<IEnumerable<ClientMenuPositionDto>> GetCateringMenu(int cateringId);
        Task<IEnumerable<CartOptionDto>> GetCartData(IEnumerable<GetCartDto> itemIds);
        Task<IEnumerable<AttachmentDto>> GetMenuPictures(int id);
        Task<ResponseDto<int>> CreateOrder(int userId, CreateOrderDto dto);
        Task OrderPaid(IdNameDto dto);
        Task RejectOrder(int id, int userId);
        Task CompleteOrder(int id, int userId);
        Task ComplainOrder(int id, string value, int userId);
        Task<int> GetOrderTotalPrice(int id, int userId);
        Task<IEnumerable<OrderDto>> GetOrders(int userId);
        Task<IEnumerable<IdNameValueDto>> GetPaymentMethods();
        Task<CateringDto> GetCatering(int orderId);
    }
}
