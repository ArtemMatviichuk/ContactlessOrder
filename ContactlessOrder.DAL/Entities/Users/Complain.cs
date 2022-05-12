using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Orders;
using System;

namespace ContactlessOrder.DAL.Entities.Users
{
    public class Complain
    {
        public int Id { get; set; }
        public ComplainStatus Status { get; set; }
        public string Content { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public int? CateringId { get; set; }
        public Catering Catering { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? ModifiedById { get; set; }
        public User ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
