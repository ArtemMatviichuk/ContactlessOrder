﻿using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            int cateringId = int.Parse(User.FindFirstValue(TokenProperties.Id));
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
            int cateringId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var menu = await _cateringService.GetOrders(cateringId);

            return Ok(menu);
        }

        [HttpGet("EndedOrders")]
        public async Task<IActionResult> GetEndedOrders()
        {
            int cateringId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var menu = await _cateringService.GetEndedOrders(cateringId);

            return Ok(menu);
        }

        [HttpGet("Modifications")]
        public async Task<IActionResult> GetModifications()
        {
            int cateringId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var modifications = await _cateringService.GetModifications(cateringId);

            return Ok(modifications);
        }

        [HttpPut("Modifications/{id}")]
        public async Task<IActionResult> UpdateModification(int id, [FromBody] UpdateCateringMenuOptionDto dto)
        {
            int cateringId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            await _cateringService.UpdateModification(id, cateringId, dto);

            return Ok();
        }
        
        [HttpGet("OrderStatuses")]
        public async Task<IActionResult> GetOrderStatuses()
        {
            var statuses = await _cateringService.GetOrderStatuses();

            return Ok(statuses);
        }

        [HttpPut("Orders/{id}/Status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, ValueDto<int> dto)
        {
            await _cateringService.UpdateOrderStatus(id, dto.Value);

            return Ok();
        }
    }
}
