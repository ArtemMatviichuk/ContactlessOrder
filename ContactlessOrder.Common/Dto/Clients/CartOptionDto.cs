using ContactlessOrder.Common.Dto.Caterings;
using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Clients
{
    public class CartOptionDto : ClientMenuOptionDto
    {
        public int CateringId { get; set; }
        public int CateringOptionId { get; set; }
        public string CompanyName { get; set; }
        public int? FirstPictureId { get; set; }
        public IEnumerable<CateringModificationDto> Modifications { get; set; }
    }
}
