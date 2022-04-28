using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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
        public async Task<IActionResult> GetCaterings([FromQuery] GetCateringsDto dto, string search = null)
        {
            var caterings = await _clientService.GetCaterings(dto, search);

            return Ok(caterings);
        }

        [HttpGet("Caterings/{id}/Menu")]
        public async Task<IActionResult> GetCateringMenu(int id)
        {
            var menu = await _clientService.GetCateringMenu(id);

            return Ok(menu);
        }

        [HttpGet("Cart")]
        public async Task<IActionResult> GetCartData([FromQuery] ValueDto<IEnumerable<GetCartDto>> dto)
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

        [HttpGet("Order")]
        public async Task<IActionResult> GetOrders()
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            var orders = await _clientService.GetOrders(userId);

            return Ok(orders);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            int id = await _clientService.CreateOrder(userId, dto);

            return Ok(id);
        }

        [HttpPut("Order")]
        public async Task<IActionResult> OrderPaid(IdNameDto dto)
        {
            await _clientService.OrderPaid(dto);

            return Ok();
        }

        [HttpGet("Order/{id}/TotalPrice")]
        public async Task<IActionResult> GetOrderTotalPrice(int id)
        {
            int userId = int.Parse(User.FindFirstValue(TokenProperties.Id));
            int totalPrice = await _clientService.GetOrderTotalPrice(id, userId);

            if (totalPrice == -1)
            {
                return BadRequest("Помилка");
            }

            return Ok(totalPrice);
        }
    }
}
