using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Caterings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactlessOrder.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CateringsController : ControllerBase
    {
        private readonly ICateringService _cateringService;

        public CateringsController(ICateringService cateringService)
        {
            _cateringService = cateringService;
        }

        [HttpGet("Menu")]
        public async Task<IActionResult> GetMenu()
        {
            int cateringId = int.Parse(User.FindFirstValue(TokenProperties.CateringId));
            var menu = await _cateringService.GetMenu(cateringId);

            return Ok(menu);
        }

        [HttpPut("Menu/{id}")]
        public async Task<IActionResult> UpdateMenuOption(int id, [FromBody] UpdateCateringMenuOptionDto dto)
        {
            var message = await _cateringService.UpdateMenuOption(id, dto);

            if (!string.IsNullOrEmpty(message))
            {
                return BadRequest(new { message });
            }

            return Ok();
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> GetOrders()
        {
            int cateringId = int.Parse(User.FindFirstValue(TokenProperties.CateringId));
            var menu = await _cateringService.GetOrders(cateringId);

            return Ok(menu);
        }
    }
}
