using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Orders
{
    public class CreateOrderDto
    {
        public int CateringId { get; set; }
        public string Comment { get; set; }

        public IEnumerable<OrderPositionDto> Positions { get; set; }
    }
}
