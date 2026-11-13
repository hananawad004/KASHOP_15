
using KASHOP.BLL.Settings;
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
     }
}
