using System.Security.Claims;
using System.Threading.Tasks;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Auth;
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

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _adminService.GetUsers();
            return Ok(users);
        }

        [HttpPost("Users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var roleValue = int.Parse(User.FindFirstValue(TokenProperties.RoleValue));

            if (roleValue != UserRoles.AdminValue)
            {
                return Forbid("Доступ заборонено");
            }

            var response = await _adminService.CreateUser(dto);

            if (response == null)
            {
                return BadRequest(new { message = "Помилка" });
            }

            return Ok(response);
        }

        [HttpPut("Users/{id}/Block")]
        public async Task<IActionResult> BlockUser(int id)
        {
            await _adminService.BlockUser(id);
            return Ok();
        }

        [HttpPut("Users/{id}/GeneratePassword")]
        public async Task<IActionResult> RegenerateUserPassword(int id)
        {
            var response = await _adminService.RegenerateUserPassword(id);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(new { message = response.ErrorMessage });
            }

            return Ok(new { password = response.Response });
        }

        [HttpDelete("Users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var message = await _adminService.DeleteUser(id, userId);

            if (!string.IsNullOrEmpty(message))
            {
                return BadRequest(new { message });
            }

            return Ok();
        }

        [HttpGet("Support")]
        public async Task<IActionResult> GetSupport()
        {
            var users = await _adminService.GetSupport();
            return Ok(users);
        }

        [HttpGet("Administrators")]
        public async Task<IActionResult> GetAdministrators()
        {
            var users = await _adminService.GetAdministrators();
            return Ok(users);
        }
    }
}
