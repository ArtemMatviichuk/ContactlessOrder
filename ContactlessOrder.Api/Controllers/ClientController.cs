using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("Caterings")]
        public async Task<IActionResult> GetCaterings([FromQuery] GetCateringsDto dto)
        {
            var caterings = await _clientService.GetCaterings(dto);

            return Ok(caterings);
        }

        [HttpGet("Caterings/{id}/Menu")]
        public async Task<IActionResult> GetCateringMenu(int id)
        {
            var menu = await _clientService.GetCateringMenu(id);

            return Ok(menu);
        }

        [HttpGet("Cart")]
        public async Task<IActionResult> GetCartData([FromQuery] ValueDto<IEnumerable<int>> dto)
        {
            var cart = await _clientService.GetCartData(dto.Value);

            return Ok(cart);
        }

        [HttpGet("Menu/{id}/Pictures")]
        public async Task<IActionResult> GetMenuPictures(int id)
        {
            var cart = await _clientService.GetMenuPictures(id);

            return Ok(cart);
        }
    }
}
