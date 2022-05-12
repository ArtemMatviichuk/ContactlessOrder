using ContactlessOrder.DAL.Entities.Companies;
using System;

namespace ContactlessOrder.DAL.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ProfilePhotoPath { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public Company Company { get; set; }
        public Catering Catering { get; set; }
    }
}
