
using KASHOP.BLL;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace KASHOP.PL.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var (success, errors) = await _authService.RegisterAsync(registerRequest);
            if (!success)
            {
                return BadRequest(errors);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var (success, errors, response) = await _authService.LoginAsync(loginRequest);
            if (!success)
            {
                return BadRequest(errors);
            }
            return Ok(response);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            Console.WriteLine($"Received UserId: {userId}");
            Console.WriteLine($"Received Token: {token}");
            var (success, errors) = await _authService.ConfirmEmailAsync(userId, token);
            if (!success)
            {
                return BadRequest(errors);
            }
            return Ok();
        }
     }
}
