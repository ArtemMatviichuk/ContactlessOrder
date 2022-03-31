using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<OrderPosition> Positions { get; set; }
    }
}
