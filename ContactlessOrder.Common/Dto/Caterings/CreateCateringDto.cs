using System;
using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Caterings
{
    public class CreateCateringDto
    {
        public string Name { get; set; }
        public string Services { get; set; }
        public CoordinateDto Coordinates { get; set; }
        public bool FullDay { get; set; }
        public TimeDto OpenTime { get; set; }
        public TimeDto CloseTime { get; set; }

        public IEnumerable<int> MenuIds { get; set; }
    }
}
