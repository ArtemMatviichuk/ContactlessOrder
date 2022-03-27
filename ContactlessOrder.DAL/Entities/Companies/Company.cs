using ContactlessOrder.DAL.Entities.Users;
using System;
using System.Collections.Generic;

namespace ContactlessOrder.DAL.Entities.Companies
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<Catering> Caterings { get; set; }
    }
}
