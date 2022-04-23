using ContactlessOrder.DAL.Entities.Companies;
using System.Collections.Generic;

namespace ContactlessOrder.DAL.Entities.Orders
{
    public class OrderPosition
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int InMomentPrice { get; set; }
        public string OptionName { get; set; }

        public int? OptionId { get; set; }
        public CateringMenuOption Option { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public IEnumerable<OrderPositionModification> Modifications { get; set; }
    }
}
