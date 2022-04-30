using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface IClientRepository : IRepositoryBase
    {
        Task<IEnumerable<Catering>> GetCateringsByCoordinates(CoordinateDto from, CoordinateDto to);
        Task<Order> GetOrder(int id);
        Task<IEnumerable<Order>> GetOrders(int userId);
        Task<IEnumerable<Order>> GetNotFinishedOrders(int userId);
        Task<Catering> GetCatering(int id);
    }
}
