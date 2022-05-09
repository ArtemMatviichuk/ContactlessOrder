using System.Security.Claims;
using System.Threading.Tasks;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace HE.Material.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("Companies")]
        public async Task<IActionResult> GetCompanies([FromQuery] bool approved)
        {
            var companies = await _adminService.GetCompanies(approved);
            return Ok(companies);
        }

        [HttpPut("Companies/{id}/Approve")]
        public async Task<IActionResult> ApproveCompany(int id)
        {
            var userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            await _adminService.ApproveCompany(id, userId);
            return Ok();
        }

        [HttpPut("Companies/{id}/Reject")]
        public async Task<IActionResult> RejectCompany(int id)
        {
            await _adminService.RejectCompany(id);
            return Ok();
        }


        [HttpGet("Companies/{id}/Caterings")]
        public async Task<IActionResult> GetCaterings(int id)
        {
            var data = await _adminService.GetCaterings(id);
            return Ok(data);
        }

        [HttpGet("PaymentData/{id}")]
        public async Task<IActionResult> GetCompanyPaymentData(int id)
        {
            var data = await _adminService.GetCompanyPaymentData(id);
            return Ok(data);
        }
    }
}
