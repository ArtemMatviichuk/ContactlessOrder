using System;

namespace ContactlessOrder.Common.Dto.Companies
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public DateTime RegisteredDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public int UserId { get; set; }
        public int? ApprovedById { get; set; }
        public string ApprovedByName { get; set; }
        public int? PaymentDataId { get; set; }
    }
}
