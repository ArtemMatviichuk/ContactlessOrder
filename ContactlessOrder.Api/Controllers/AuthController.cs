using System.Threading.Tasks;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Users;
using Microsoft.AspNetCore.Mvc;

namespace HE.Material.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(UserLoginRequestDto dto)
        {
            var response = await _authService.Authenticate(dto);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return Ok(new { message = response.ErrorMessage });
            }

            return Ok(new { Token = response.Response });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequestDto dto)
        {
            var response = await _authService.Register(dto);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return Ok(new { message = response.ErrorMessage });
            }

            return Ok(new { Token = response.Response });
        }
    }
}
