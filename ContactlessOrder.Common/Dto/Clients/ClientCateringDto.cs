using ContactlessOrder.Common.Dto.Caterings;

namespace ContactlessOrder.Common.Dto.Clients
{
    public class ClientCateringDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Services { get; set; }
        public bool FullDay { get; set; }
        public TimeDto OpenTime { get; set; }
        public TimeDto CloseTime { get; set; }
        public CoordinateDto Coordinates { get; set; }
    }
}
