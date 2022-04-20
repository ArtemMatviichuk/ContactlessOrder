﻿using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.Common.Dto.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface ICateringService
    {
        Task<IEnumerable<CateringMenuOptionDto>> GetMenu(int cateringId);
        Task<IEnumerable<MenuItemDto>> GetModifications(int cateringId);
        Task<string> UpdateMenuOption(int id, UpdateCateringMenuOptionDto dto);
        Task<IEnumerable<OrderDto>> GetOrders(int cateringId);
    }
}
