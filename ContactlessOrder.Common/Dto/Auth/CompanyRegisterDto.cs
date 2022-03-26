namespace ContactlessOrder.Common.Dto.Auth
{
    public class CompanyRegisterRequestDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
