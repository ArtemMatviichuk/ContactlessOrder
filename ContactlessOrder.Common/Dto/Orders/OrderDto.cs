using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.Common.Dto.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int TotalCost { get; set; }
        public string Comment { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int StatusValue { get; set; }

        public IEnumerable<OrderPositionDto> Positions { get; set; }
    }
}
