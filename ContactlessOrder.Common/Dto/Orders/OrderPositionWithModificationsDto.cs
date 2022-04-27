using ContactlessOrder.Common.Dto.Common;
using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Orders
{
    public class OrderPositionWithModificationsDto
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public int Quantity { get; set; }
        public int? PictureId { get; set; }
        public IEnumerable<IdNameDto> Modifications { get; set; }
    }
}
