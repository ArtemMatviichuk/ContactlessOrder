namespace ContactlessOrder.Common.Dto.Auth
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleValue { get; set; }
    }
}
