using System;

namespace ContactlessOrder.Common.Dto.Caterings
{
    public class CreateCateringDto
    {
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public bool FullDay { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
    }
}
