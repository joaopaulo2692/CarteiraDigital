using CarteiraDigital.Application.DTOs.Auth;
using CarteiraDigital.Application.DTOs.Users;
using CarteiraDigital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraDigital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(CreateUserRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
        }
    }
}
