using System;
using System.Collections.Generic;

namespace ContactlessOrder.DAL.Entities.Companies
{
    public class Catering
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Services { get; set; }
        public bool FullDay { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }

        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public DateTime RegisteredDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int? CoordinatesId { get; set; }
        public Coordinate Coordinates { get; set; }

        public ICollection<CateringMenuOption> MenuOptions { get; set; }
        public ICollection<CateringMenuModification> MenuModifications { get; set; }
    }
}
