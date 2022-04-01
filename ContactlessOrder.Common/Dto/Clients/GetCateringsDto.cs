using ContactlessOrder.Common.Dto.Caterings;

namespace ContactlessOrder.Common.Dto.Clients
{
    public class GetCateringsDto
    {
        public CoordinateDto From { get; set; }
        public CoordinateDto To { get; set; }
    }
}
