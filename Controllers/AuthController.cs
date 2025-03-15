using Microsoft.AspNetCore.Mvc;
using API_FEB.DTOs;
using System.Threading.Tasks;

namespace API_FEB.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // ✅ Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
        {
            var result = await _authService.RegisterUser(userCreateDto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message, user = result.Data });
        }

        // ✅ Login an existing user (Fixed)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginUser(loginDto.Email, loginDto.Password);

            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new { message = result.Message, user = result.Data });
        }
    }
}
