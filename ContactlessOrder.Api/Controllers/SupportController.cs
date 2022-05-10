using System.Threading.Tasks;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Common;
using Microsoft.AspNetCore.Mvc;

namespace HE.Material.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        [HttpGet("Complains/{statusValue}")]
        public async Task<IActionResult> GetComplains(int statusValue)
        {
            var complains = await _supportService.GetComplains(statusValue);
            return Ok(complains);
        }

        [HttpPut("Complains/{id}/Status")]
        public async Task<IActionResult> ChangeComplainStatus(int id, ValueDto<int> dto)
        {
            await _supportService.ChangeComplainStatus(id, dto.Value);
            return Ok();
        }
    }
}
