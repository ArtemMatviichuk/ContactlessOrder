using ContactlessOrder.DAL.Entities.Users;
using System.Collections.Generic;

namespace ContactlessOrder.DAL.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string PaymentNumber { get; set; }

        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderPosition> Positions { get; set; }
    }
}
