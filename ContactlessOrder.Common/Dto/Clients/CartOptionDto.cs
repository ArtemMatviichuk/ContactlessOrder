namespace ContactlessOrder.Common.Dto.Clients
{
    public class CartOptionDto : ClientMenuOptionDto
    {
        public int CateringId { get; set; }
        public string CompanyName { get; set; }
        public int? FirstPictureId { get; set; }
    }
}
