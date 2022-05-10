using System;

namespace ContactlessOrder.Common.Dto.Orders
{
    public class ComplainDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string Content { get; set; }

        public int? OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int? CateringId { get; set; }
        public string CateringName { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
