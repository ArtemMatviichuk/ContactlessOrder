using ContactlessOrder.Common.Dto.Auth;
using Microsoft.AspNetCore.Http;

namespace ContactlessOrder.Common.Dto.Companies
{
    public class UpdateCompanyDataDto : CompanyRegisterRequestDto
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public bool RemoveLogo { get; set; }
        public IFormFile Logo { get; set; }
    }
}
