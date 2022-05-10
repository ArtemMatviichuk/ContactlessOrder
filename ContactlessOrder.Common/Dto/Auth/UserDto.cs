using System;

namespace ContactlessOrder.Common.Dto.Auth
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBlocked { get; set; }
        public bool EmailConfirmed { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int? CompanyId { get; set; }
        public int RoleValue { get; set; }
    }
}
