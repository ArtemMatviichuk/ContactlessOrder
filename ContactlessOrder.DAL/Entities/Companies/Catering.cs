using System;

namespace ContactlessOrder.DAL.Entities.Companies
{
    public class Catering
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public bool FullDay { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }

        public DateTime RegisteredDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
