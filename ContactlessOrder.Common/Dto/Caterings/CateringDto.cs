using System;

namespace ContactlessOrder.Common.Dto.Caterings
{
    public class CateringDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Services { get; set; }
        public bool FullDay { get; set; }
        public TimeDto OpenTime { get; set; }
        public TimeDto CloseTime { get; set; }
        public string Login { get; set; }

        public int CompanyId { get; set; }
        public CoordinateDto Coordinates { get; set; }
    }
}
