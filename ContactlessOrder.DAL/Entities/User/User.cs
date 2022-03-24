using System;

namespace ContactlessOrder.DAL.Entities.User
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public int StatusId { get; set; }
        public UserStatus Status { get; set; }
    }
}
