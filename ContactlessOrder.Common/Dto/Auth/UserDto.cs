using System;

namespace ContactlessOrder.Common.Dto.Auth
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public int? CompanyId { get; set; }
    }
}
