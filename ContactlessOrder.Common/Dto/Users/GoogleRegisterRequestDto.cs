namespace ContactlessOrder.Common.Dto.Users
{
    public class GoogleRegisterRequestDto
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
        public string PhoneNumber { get; set; }
    }
}
