using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Companies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactlessOrder.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompany()
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var company = await _companyService.GetCompany(userId);

            return Ok(company);
        }

        [HttpGet("Logo")]
        public async Task<IActionResult> GetCompanyLogo()
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var logo = await _companyService.GetCompanyLogo(userId);

            if (logo == null)
            {
                return NotFound(new { message = "File not found." });
            }

            return File(logo.Bytes, logo.ContentType, logo.FileName);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCompanyData([FromForm] UpdateCompanyDataDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var message = await _companyService.UpdateCompanyData(userId, dto);

            if (!string.IsNullOrEmpty(message))
            {
                return BadRequest(new { message });
            }

            return Ok();
        }

        [HttpGet("Caterings")]
        public async Task<IActionResult> GetCaterings()
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var caterings = await _companyService.GetCaterings(userId);

            return Ok(caterings);
        }

        [HttpPost("Caterings")]
        public async Task<IActionResult> CreateCatering(CreateCateringDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var response = await _companyService.CreateCatering(userId, dto);

            if (response == null)
            {
                return BadRequest("Точка не знайдена");
            }

            return Ok(response);
        }

        [HttpPut("Caterings/{id}")]
        public async Task<IActionResult> UpdateCatering(int id, CreateCateringDto dto)
        {
            await _companyService.UpdateCatering(id, dto);

            return Ok();
        }

        [HttpDelete("Caterings/{id}")]
        public async Task<IActionResult> DeleteCatering(int id)
        {
            await _companyService.DeleteCatering(id);

            return Ok();
        }

        [HttpPut("Caterings/{id}/RegeneratePassword")]
        public async Task<IActionResult> RegenerateCateringPassword(int id)
        {
            var message = await _companyService.RegenerateCateringPassword(id);

            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Точка не знайдена");
            }

            return Ok(new { password = message });
        }

        [HttpGet("Menu")]
        public async Task<IActionResult> GetMenu()
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var menu = await _companyService.GetMenu(userId);

            return Ok(menu);
        }

        [HttpGet("MenuOptions")]
        public async Task<IActionResult> GetMenuOptions()
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var menu = await _companyService.GetMenuOptions(userId);

            return Ok(menu);
        }

        [HttpPost("Menu")]
        public async Task<IActionResult> CreateMenu([FromForm] CreateMenuItemDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            await _companyService.CreateMenuItem(userId, dto);

            return Ok();
        }

        [HttpPut("Menu/{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromForm] UpdateMenuItemDto dto)
        {
            await _companyService.UpdateMenuItem(id, dto);

            return Ok();
        }

        [HttpDelete("Menu/{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            await _companyService.DeleteMenuItem(id);

            return Ok();
        }

        [HttpGet("Menu/{id}/Pictures")]
        public async Task<IActionResult> GetToolPictures(int id)
        {
            var images = await _companyService.GetMenuItemPictures(id);

            return Ok(images);
        }

        [AllowAnonymous]
        [HttpGet("Menu/Pictures/{id}/File")]
        public async Task<IActionResult> GetPictureFile(int id)
        {
            var file = await _companyService.GetMenuItemPictureFile(id);

            if (file == null)
            {
                return NotFound(new { message = "File not found." });
            }

            return File(file.Bytes, file.ContentType, file.FileName);
        }
    }
}
