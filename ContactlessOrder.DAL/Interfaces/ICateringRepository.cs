using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface ICateringRepository : IRepositoryBase
    {
        Task<IEnumerable<CateringMenuOption>> GetMenu(int cateringId);
        Task<CateringMenuOption> GetMenuOption(int id);
        Task<IEnumerable<CateringMenuOption>> GetMenuOptions(int cateringId, IEnumerable<int> ids);
        Task<IEnumerable<Order>> GetOrders(int cateringId);
        Task<IEnumerable<MenuItemPicture>> GetMenuItemPictures(int id);
    }
}
