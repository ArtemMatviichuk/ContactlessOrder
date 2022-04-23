using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Orders
{
    public class OrderPositionDto
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public int Quantity { get; set; }
        public int? PictureId { get; set; }
        public IEnumerable<int> ModificationIds { get; set; }
    }
}
