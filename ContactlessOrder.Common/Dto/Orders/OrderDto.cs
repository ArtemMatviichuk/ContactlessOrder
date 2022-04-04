using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int TotalPrice { get; set; }
        public string Comment { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int StatusValue { get; set; }

        public IEnumerable<OrderPositionDto> Positions { get; set; }
    }
}
